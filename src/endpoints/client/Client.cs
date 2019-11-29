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
      middleware.SetInputHandler(HandleInput);
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