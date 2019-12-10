using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace tcg
{
  class MiddlewareNetwork : Middleware
  {
    List<Action<int, string>> onDataListeners = new List<Action<int, string>>();
    string ipAddress;
    int port;
    public MiddlewareNetwork(string ipAddress, int port, bool listening)
    {
      this.ipAddress = ipAddress;
      this.port = port;

      if (listening)
        HandleConnectionsAsync().ContinueWith(t => Console.WriteLine("Task completed"));
    }

    private async Task HandleConnectionsAsync()
    {
      var server = new TcpListener(IPAddress.Parse(ipAddress), port);
      server.Start();

      Console.WriteLine(string.Format("Listener started at {0}:{1}", ipAddress, port));

      while (true)
      {
        var client = await server.AcceptTcpClientAsync().ConfigureAwait(false);
        Console.WriteLine("Client connected");

        var str = "no response";
        using (NetworkStream stream = client.GetStream())
        {
          byte[] data = new byte[1024];
          using (MemoryStream ms = new MemoryStream())
          {
            int numBytesRead;
            Console.WriteLine("Start data gathering");
            while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
            {
              Console.WriteLine("New data!");
              ms.Write(data, 0, numBytesRead);
            }

            str = System.Text.Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
            Console.WriteLine(string.Format("Got string: {0}", str));

            var address = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            DispatchInputData(str, address);
          }
        }
      }
    }

    private void DispatchInputData(string data, string address)
    {
      var relativeMiddlewareIndex = connectedMiddleware.FindIndex(
        mid => (mid is MiddlewareNetwork middlewareNetwork) ? middlewareNetwork.ipAddress == address : false
      );

      onDataListeners.ForEach(listener => listener(relativeMiddlewareIndex, data));
    }

    public override void ConnectMiddleware(IMiddleware middleware)
    {
      this.connectedMiddleware.Add(middleware);
    }

    public override void AddInputHandler(Action<int, string> handler)
    {
      onDataListeners.Add(handler);
    }

    public override void SendData(string data)
    {
      for (int i = 0; i < connectedMiddleware.Count; i++)
        SendDataPersonally(data, i);
    }

    public override void SendDataPersonally(string data, int receiverIndex)
    {
      if (connectedMiddleware[receiverIndex] is MiddlewareNetwork middleware)
      {
        TcpClient client = new TcpClient(middleware.ipAddress, middleware.port);
        NetworkStream stream = client.GetStream();

        byte[] dataBinary = System.Text.Encoding.ASCII.GetBytes(data);

        stream.Write(dataBinary);
        stream.Close();
      }

      if (onSendDataListeners.ContainsKey(receiverIndex))
        onSendDataListeners[receiverIndex](data);
    }
  }
}