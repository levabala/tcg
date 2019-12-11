using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  using RootAction = Action<GameState>;

  class Host
  {
    public GameState state;
    readonly List<IMiddleware> playersMiddleware;
    readonly IMiddleware middleware;
    List<Action<int, string>> inputHadlers = new List<Action<int, string>>();
    public Host(IMiddleware middleware, IEnumerable<IMiddleware> playersMiddleware)
    {
      this.middleware = middleware;
      this.playersMiddleware = playersMiddleware.ToList();

      // this.playersMiddleware.ForEach(mid => this.middleware.ConnectMiddleware(mid));
      this.middleware.AddInputHandler(HandleInput);
    }

    public void StartGame()
    {
      ShareGameState();
    }

    private void ShareGameState()
    {
      // TODO: get here count of playing players
      for (int i = 0; i < 2; i++)
      {
        SendDataToPlayer(GameStateVisualizer.GameStateToString(state, i), i);
      }
    }

    public void HandleInput(int playerIndex, string input)
    {
      // Console.WriteLine(string.Format("-- message from {0}: {1}", playerIndex, input));
      inputHadlers.ForEach(handler => handler(playerIndex, input));

      ResponseType validationStatus;
      string message;
      (validationStatus, message) = ProcessInput(playerIndex, input);

      if (validationStatus == ResponseType.Error)
        SendDataToPlayer(message, playerIndex);
      else
        ShareGameState();
    }

    public void SendDataToPlayer(string data, int playerIndex)
    {
      middleware.SendDataPersonally(data, playerIndex);
    }

    public void SendData(string data)
    {
      middleware.SendData(data);
    }

    public void AddInputHandler(Action<int, string> handler)
    {
      inputHadlers.Add(handler);
    }

    // TODO: add input json parsing (and create json configuration)
    public (ResponseType, string) ProcessInput(int playerIndex, string input)
    {
      try
      {
        if (playerIndex != state.CurrentPlayer.Id)
          throw new Exception("That's not your turn now");

        RootAction action = ParseInput(input);
        state = GameLoop.Execute(state, action);
      }
      catch (Exception e)
      {
        return (ResponseType.Error, e.Message);
      }

      return (ResponseType.Success, "success");
    }

    private RootAction ParseInput(string input)
    {
      // command example: "attack 1 3"
      // scheme: "<COMMAND_NAME> <?ARG_1> <?ARG_2> ... <?ARG_N>"
      // ? means optional parametr

      Dictionary<string, ActionType> commandsMap = new Dictionary<string, ActionType>() {
            {"attack", ActionType.Attack},
            {"heal", ActionType.Heal},
            {"play", ActionType.PlayCard},
            {"end", ActionType.EndTurn}
            // ect...
          };

      string[] chunks = input.Split(' ');
      string commandName = chunks[0];

      if (!commandsMap.ContainsKey(commandName))
        throw new ArgumentException("Command name is not listed in commandsMap");

      ActionType actionType = commandsMap[commandName];

      int[] args = chunks.Skip(1).Select(s => int.Parse(s)).ToArray();

      RootAction rootAction = ActionSet.PackAction(state, actionType, args);


      return rootAction;
    }
  }

  enum ResponseType
  {
    Success,
    Error
  }
}