using System;
using System.Collections.Generic;

namespace tcg
{
  class MiddlewareLocal : Middleware
  {
    MiddlewareLocal middleware2;
    Action<string> onSendDataListener;
    public MiddlewareLocal()
    {
    }

    public void AssignSecondMiddleware(MiddlewareLocal middleware2)
    {
      this.middleware2 = middleware2;
    }

    private void CheckIfSendMiddlewareIsAssigned()
    {
      if (middleware2 == null)
        throw new Exception("Second middleware is not assigned");
    }

    public void SendData(string data)
    {
      CheckIfSendMiddlewareIsAssigned();

      // dispatch send data event
      onSendDataListener(data);
    }
    public void SetInputHandler(Action<string> handler)
    {
      CheckIfSendMiddlewareIsAssigned();

      middleware2.onSendDataListener = handler;
    }
  }
}