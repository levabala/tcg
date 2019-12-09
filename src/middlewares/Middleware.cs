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

  abstract class Middleware<MiddlewareType> where MiddlewareType : IMiddleware
  {
    public List<Action<string>> onSendDataListeners = new List<Action<string>>();
    protected List<MiddlewareType> connectedMiddleware = new List<MiddlewareType>();
    abstract public void SendData(string data);
    abstract public void SendDataPersonally(string data, int receiverIndex);
    abstract public void AddInputHandler(Action<int, string> handler);
    abstract public void ConnectMiddleware(MiddlewareType middleware);

    // TODO: realize MiddlewareNetwork
  }
}