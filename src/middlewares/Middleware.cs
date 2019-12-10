using System;
using System.Collections.Generic;

namespace tcg
{
  interface IMiddleware
  {
    void SendData(string data);
    void SendDataPersonally(string data, int receiverIndex);
    void AddInputHandler(Action<int, string> handler);
    void ConnectMiddleware(IMiddleware middleware);
    void AddOnSendDataListener(int playerIndex, Action<string> listener);
    void AddOnSendDataListenerNext(Action<string> listener);
  }

  abstract class Middleware : IMiddleware
  {
    public Dictionary<int, Action<string>> onSendDataListeners = new Dictionary<int, Action<string>>();
    protected List<IMiddleware> connectedMiddleware = new List<IMiddleware>();
    abstract public void SendDataPersonally(string data, int receiverIndex);
    abstract public void AddInputHandler(Action<int, string> handler);
    abstract public void ConnectMiddleware(IMiddleware middleware);
    abstract public void SendData(string data);
    virtual public void AddOnSendDataListener(int playerIndex, Action<string> listener)
    {
      onSendDataListeners[playerIndex] = listener;
    }
    virtual public void AddOnSendDataListenerNext(Action<string> listener)
    {
      int playerIndex = onSendDataListeners.Count;
      onSendDataListeners[playerIndex] = listener;
    }
  }
}