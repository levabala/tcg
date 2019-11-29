using System;

namespace tcg
{
  interface Middleware
  {
    void SendData(string data);
    void SetInputHandler(Action<string> handler);
  }
}