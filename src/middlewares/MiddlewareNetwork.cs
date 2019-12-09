using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace tcg
{
  class MiddlewareNetwork : Middleware<MiddlewareNetwork>, IMiddleware
  {
    List<Action<int, string>> onDataListeners = new List<Action<int, string>>();
    string ipAddress;
    int port;
    public MiddlewareNetwork(string ipAddress, int port, bool listening)
    {
      this.ipAddress = ipAddress;
      this.port = port;

      if (listening)
        HandleConnectionsAsync().Wait();
    }

    private async Task HandleConnectionsAsync()
    {
      var server = new TcpListener(IPAddress.Parse(ipAddress), port);
      server.Start();

      while (true)
      {
        var client = await server.AcceptTcpClientAsync().ConfigureAwait(false);
        NetworkStream stream = client.GetStream();
      }
    }

    public override void ConnectMiddleware(MiddlewareNetwork middleware)
    {
      this.connectedMiddleware.Add(middleware);
    }

    public override void AddInputHandler(Action<int, string> handler)
    {
      onDataListeners.Add(handler);
    }

    public override void SendDataPersonally(string data, int receiverIndex)
    {
      MiddlewareNetwork middleware = connectedMiddleware[receiverIndex];
      TcpClient client = new TcpClient(middleware.ipAddress, port);

      NetworkStream stream = client.GetStream();
      byte[] dataBinary = System.Text.Encoding.ASCII.GetBytes(data);

      stream.Write(dataBinary);
      stream.Close();
    }
  }
}