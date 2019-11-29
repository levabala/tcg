using System;
using System.Collections.Generic;

namespace tcg
{
  abstract class Middleware
  {
    public List<Action<string>> onSendDataListeners = new List<Action<string>>();
    protected List<Middleware> connectedMiddleware = new List<Middleware>();
    abstract public void SendData(string data);
    abstract public void SendDataPersonally(string data, int receiverIndex);
    abstract public void AddInputHandler(Action<int, string> handler);
    abstract public void ConnectMiddleware(Middleware middleware);
  }
}