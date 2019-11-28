using System;
using System.Collections.Generic;

namespace tcg
{
  class Host
  {
    public Host()
    {

    }

    public (ResponseType, string) HandleInput(string input)
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