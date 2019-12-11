using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace tcg
{
  class Program
  {
    static void Main(string[] args)
    {

      Middleware myMiddleware;
      var isHost = args[0] == "-h" || args[0] == "--host";
      if (isHost)
      {
        myMiddleware = new MiddlewareLocal();

        var otherPlayerAddress = args[1];
        var arr = otherPlayerAddress.Split(":");
        var playerIp = arr[0];
        var playerPort = int.Parse(arr[1]);

        var playerMiddleware = new MiddlewareNetwork(playerIp, playerPort, false);

        var port = 3001;
        var middHost = new MiddlewareNetwork("127.0.0.1", port, true);

        middHost.AddInputHandler((i, s) => Console.WriteLine(string.Format("Host got message from {0}: {1}", i, s)));

        middHost.ConnectMiddleware(myMiddleware);
        myMiddleware.ConnectMiddleware(middHost);

        Host h = new Host(middHost, new List<IMiddleware> {
          myMiddleware,
          playerMiddleware,
        });
      }
      else
      {
        var serverAddress = args[0];
        var arr = serverAddress.Split(":");
        var serverIp = arr[0];
        var serverPort = int.Parse(arr[1]);

        var port = 3002;
        myMiddleware = new MiddlewareNetwork("127.0.0.1", port, true);
        var serverMiddleware = new MiddlewareNetwork(serverIp, serverPort, false);

        myMiddleware.ConnectMiddleware(serverMiddleware);
        serverMiddleware.ConnectMiddleware(myMiddleware);
      }

      myMiddleware.AddInputHandler((i, s) => Console.WriteLine(string.Format("You got messag from host {0}", s)));

      while (true)
      {
        var command = Console.ReadLine();
        myMiddleware.SendData(command);
      }
    }
  }
}
