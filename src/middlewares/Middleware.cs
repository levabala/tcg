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
    void AddOnSendDataListener(Action<string> listener);
    void ReceiveData(string data, IMiddleware sender);
  }

  abstract class Middleware : IMiddleware
  {
    public List<Action<string>> onSendDataListeners = new List<Action<string>>();
    public List<Action<int, string>> onRecieveDataListeners = new List<Action<int, string>>();
    protected List<IMiddleware> connectedMiddleware = new List<IMiddleware>();
    abstract public void SendDataPersonally(string data, int receiverIndex);
    abstract public void ConnectMiddleware(IMiddleware middleware);
    abstract public void SendData(string data);
    virtual public void AddOnSendDataListener(Action<string> listener)
    {
      onSendDataListeners.Add(listener);
    }
    virtual public void AddInputHandler(Action<int, string> handler)
    {
      onRecieveDataListeners.Add(handler);
    }
    virtual public void ReceiveData(string data, IMiddleware sender)
    {
      var senderIndex = connectedMiddleware.FindIndex(s => s == sender);
      onRecieveDataListeners.ForEach(listener => listener(senderIndex, data));
    }
  }
}