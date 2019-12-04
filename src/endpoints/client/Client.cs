using System;
using System.Collections.Generic;

namespace tcg
{
  class Client
  {
    readonly Middleware middleware;
    public Client(Middleware middleware)
    {
      this.middleware = middleware;
      middleware.AddInputHandler((i, input) => HandleInput(input));
    }

    public void HandleInput(string input)
    {
      // just print everything we've got
      Console.WriteLine(input);
    }

    public void SendCommand(string command)
    {
      middleware.SendData(command);
    }
  }
}