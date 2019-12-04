using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  using RootAction = Action<GameState>;

  class Host
  {
    public GameState state;
    readonly List<Middleware> playersMiddleware;
    readonly Middleware middleware;
    List<Action<int, string>> inputHadlers = new List<Action<int, string>>();
    public Host(Middleware middleware, IEnumerable<Middleware> playersMiddleware)
    {
      this.middleware = middleware;
      this.playersMiddleware = playersMiddleware.ToList();

      // this.playersMiddleware.ForEach(mid => this.middleware.ConnectMiddleware(mid));
      this.middleware.AddInputHandler(HandleInput);
    }

    public void HandleInput(int playerIndex, string input)
    {
      inputHadlers.ForEach(handler => handler(playerIndex, input));

      ResponseType validationStatus;
      string message;
      (validationStatus, message) = ProcessInput(playerIndex, input);

      if (validationStatus == ResponseType.Error)
        SendDataToPlayer(message, playerIndex);
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
        RootAction action = ParseInput(input);
        state = GameLoop.Execute(state, action);
      }
      catch (ArgumentException e)
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
      int actualArgsCount = GetActualArgsCount(actionType);

      int[] actualArgs = args.Take(actualArgsCount).ToArray();
      int[] remainArgs = args.Skip(actualArgsCount).Take(args.Length - actualArgsCount).ToArray();

      RootAction rootAction = ActionSet.PackAction(state, actionType, actualArgs, remainArgs);


      return rootAction;
    }

    private int GetActualArgsCount(ActionType actionType)
    {
      Delegate action = ActionSet.Actions[actionType];
      switch (action)
      {
        case SpecifiedAction a:
          return 0;
        case SpecifiedAction<int> a:
          return 1;
        case SpecifiedAction<int, int> a:
          return 2;
        case SpecifiedAction<int, int, int> a:
          return 3;
        case SpecifiedAction<int, int, int, int> a:
          return 4;
        case SpecifiedAction<int, int, int, int, int> a:
          return 5;
        case SpecifiedAction<int, int, int, int, int, int> a:
          return 6;
        case SpecifiedAction<int, int, int, int, int, int, int> a:
          return 7;
        case SpecifiedAction<int, int, int, int, int, int, int, int> a:
          return 8;
        case SpecifiedAction<int, int, int, int, int, int, int, int, int> a:
          return 9;
        default:

          throw new ArgumentException("Cannot find a type of the action");
      }
    }
  }

  enum ResponseType
  {
    Success,
    Error
  }
}