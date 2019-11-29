using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  class Host
  {
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
      {

      }
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
        PlayerAction action = ParseInput(input);
      }
      catch (Exception e)
      {
        return (ResponseType.Error, e.Message);
      }

      return (ResponseType.Success, "success");
    }

    private PlayerAction ParseInput(string input)
    {
      ActionType actionType = GetActionType(input);

      return new PlayerAction(0, actionType);
    }

    private ActionType GetActionType(string input)
    {
      // command example: "attack 1 3"
      // scheme: "<COMMAND_NAME> <?ARG_1> <?ARG_2>"
      // ? means optional parametr

      Dictionary<string, ActionType> commandsMap = new Dictionary<string, ActionType>() {
            {"attack", ActionType.Attack},
            // ect...
          };

      string[] chunks = input.Split(' ');
      string commandName = chunks[0];

      if (!commandsMap.ContainsKey(commandName))
        throw new ArgumentException("Command name is not listed in commandsMap");

      ActionType actionType = commandsMap[commandName];

      return actionType;
    }
  }

  enum ResponseType
  {
    Success,
    Error
  }
}