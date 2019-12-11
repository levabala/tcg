using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  class MiddlewareLocal : Middleware
  {


    public override void ConnectMiddleware(IMiddleware middleware)
    {
      this.connectedMiddleware.Add(middleware);
    }

    public void ConnectMiddlewareTogether(MiddlewareLocal middleware)
    {
      this.connectedMiddleware.Add(middleware);
      middleware.ConnectMiddleware(this);
    }

    public override void SendDataPersonally(string data, int receiverIndex)
    {
      connectedMiddleware[receiverIndex].ReceiveData(data, this);
    }

    public override void SendData(string data)
    {
      connectedMiddleware.ForEach(midd => midd.ReceiveData(data, this));
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