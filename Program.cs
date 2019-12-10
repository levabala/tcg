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
      var myMiddleware = new MiddlewareLocal();

      var isHost = args[0] == "-h" || args[0] == "--host";
      if (isHost)
      {
        var otherPlayerAddress = args[1];
        var arr = otherPlayerAddress.Split(":");
        var playerIp = arr[0];
        var playerPort = int.Parse(arr[1]);

        var playerMiddleware = new MiddlewareNetwork(playerIp, playerPort, false);

        var port1 = 3001;
        var middHost = new MiddlewareNetwork("127.0.0.1", port1, true);

        middHost.ConnectMiddleware(myMiddleware);

        Host h = new Host(middHost, new List<IMiddleware> {
          myMiddleware,
          playerMiddleware,
        });
      }
    }
  }
}
