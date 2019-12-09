using System;
using System.Collections.Generic;

namespace tcg
{
  interface IMiddleware
  {
    void SendData(string data);
    void SendDataPersonally(string data, int receiverIndex);
    void AddInputHandler(Action<int, string> handler);
    // void ConnectMiddleware(IMiddleware middleware);
  }

  abstract class Middleware<ConnectableMiddlewareType> where ConnectableMiddlewareType : IMiddleware
  {
    public List<Action<string>> onSendDataListeners = new List<Action<string>>();
    protected List<ConnectableMiddlewareType> connectedMiddleware = new List<ConnectableMiddlewareType>();
    abstract public void SendDataPersonally(string data, int receiverIndex);
    abstract public void AddInputHandler(Action<int, string> handler);
    abstract public void ConnectMiddleware(ConnectableMiddlewareType middleware);
    virtual public void SendData(string data)
    {
      for (int i = 0; i < onSendDataListeners.Count; i++)
        SendDataPersonally(data, i);
    }
  }
}