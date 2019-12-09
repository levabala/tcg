using System;
using System.Collections.Generic;

namespace tcg
{
  class Client
  {
    readonly IMiddleware IMiddleware;
    public Client(IMiddleware IMiddleware)
    {
      this.IMiddleware = IMiddleware;
      IMiddleware.AddInputHandler((i, input) => HandleInput(input));
    }

    public void HandleInput(string input)
    {
      // just print everything we've got
      Console.WriteLine(input);
    }

    public void SendCommand(string command)
    {
      IMiddleware.SendData(command);
    }
  }
}