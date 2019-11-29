using System;
using System.Collections.Generic;

namespace tcg
{
  class Host
  {
    readonly Middleware middlewarePlayer1, middlewarePlayer2;
    List<Action<int, string>> inputHadlers = new List<Action<int, string>>();
    public Host(Middleware middlewarePlayer1, Middleware middlewarePlayer2)
    {
      this.middlewarePlayer1 = middlewarePlayer1;
      this.middlewarePlayer2 = middlewarePlayer2;

      // it's function composition
      // we use lambda functions to add an additional argumnet - playerIndex
      // for first player input index is 1 (second player)
      // for second one it's 0 (first player)
      middlewarePlayer1.SetInputHandler((input) => HandleInput(1, input));
      middlewarePlayer2.SetInputHandler((input) => HandleInput(0, input));
    }

    public void HandleInput(int playerIndex, string input)
    {
      inputHadlers.ForEach(handler => handler(playerIndex, input));
    }

    public void AddHandlerInput(Action<int, string> handler)
    {
      inputHadlers.Add(handler);
    }

    // TODO: add input json parsing (and create json configuration)
    public (ResponseType, string) ProcessInput(string input)
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