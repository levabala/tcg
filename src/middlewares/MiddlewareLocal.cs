using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  class MiddlewareLocal : Middleware<MiddlewareLocal>, IMiddleware
  {
    public override void ConnectMiddleware(MiddlewareLocal middleware)
    {
      this.connectedMiddleware.Add(middleware);
    }

    public void ConnectMiddlewareTogether(MiddlewareLocal middleware)
    {
      this.connectedMiddleware.Add(middleware);
      middleware.ConnectMiddleware(this);
    }

    public override void SendData(string data)
    {
      for (int i = 0; i < onSendDataListeners.Count; i++)
        SendDataPersonally(data, i);
    }

    public override void SendDataPersonally(string data, int receiverIndex)
    {
      // dispatch send data event
      onSendDataListeners[receiverIndex](data);
    }

    public override void AddInputHandler(Action<int, string> handler)
    {
      for (int i = 0; i < connectedMiddleware.Count; i++)
      {
        int localI = i;
        Action<string> closure = input => handler(localI, input);

        // var closure = ((Func<Action<string>>)(() =>
        // {
        //   int localI = i;
        //   return input => handler(localI, input);
        // }))();

        connectedMiddleware[i].onSendDataListeners.Add(closure);
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