using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  class MiddlewareLocal : Middleware
  {
    public MiddlewareLocal()
    {

    }

    public override void ConnectMiddleware(Middleware middleware)
    {
      this.connectedMiddleware.Add(middleware);
    }

    public void ConnectMiddlewareTogether(MiddlewareLocal middleware)
    {
      this.connectedMiddleware.Add(middleware);
      middleware.ConnectMiddleware(this);
    }

    private void CheckIfSendMiddlewareIsAssigned()
    {
      if (connectedMiddleware.Count == 0)
        throw new Exception("No middlewares connected");
    }

    public override void SendData(string data)
    {
      CheckIfSendMiddlewareIsAssigned();

      // dispatch send data event
      onSendDataListeners.ForEach(listener => listener(data));
    }
    public override void AddInputHandler(Action<int, string> handler)
    {
      CheckIfSendMiddlewareIsAssigned();

      for (int i = 0; i < connectedMiddleware.Count; i++)
      {
        connectedMiddleware[i].onSendDataListeners.Add(input => handler(i, input));
      }
    }

    public static (MiddlewareLocal, MiddlewareLocal) GenerateLinkedMiddlewareLocalPair()
    {
      MiddlewareLocal mid1 = new MiddlewareLocal();
      MiddlewareLocal mid2 = new MiddlewareLocal();

      // assign them together
      mid1.ConnectMiddlewareTogether(mid2);

      return (mid1, mid2);
    }

    public static (MiddlewareLocal, MiddlewareLocal, MiddlewareLocal) GenerateLinkedMiddlewareLocalTriple()
    {
      MiddlewareLocal mid1 = new MiddlewareLocal();
      MiddlewareLocal mid2 = new MiddlewareLocal();
      MiddlewareLocal midHost = new MiddlewareLocal();

      // assign them together
      midHost.ConnectMiddlewareTogether(mid1);
      midHost.ConnectMiddlewareTogether(mid2);

      return (mid1, mid2, midHost);
    }
  }
}