using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace tcg
{
  class Program
  {
    public static string GetLocalIPAddress()
    {
      var host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (var ip in host.AddressList)
        if (ip.AddressFamily == AddressFamily.InterNetwork)
          return ip.ToString();

      throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    static void Main(string[] args)
    {
      Middleware myMiddleware;
      Host h = null;
      var isHost = args[0] == "-h" || args[0] == "--host";
      var isLocal = isHost ? (args.Length > 2 && args[2] == "--local") : (args.Length > 1 && args[1] == "--local");

      Console.WriteLine(string.Format("Session started as {0}", isHost ? "host" : "client"));

      if (isHost)
      {
        myMiddleware = new MiddlewareLocal();

        var otherPlayerAddress = args[1];
        var arr = otherPlayerAddress.Split(":");
        var playerIp = arr[0];
        var playerPort = int.Parse(arr[1]);

        var playerMiddleware = new MiddlewareNetwork(playerIp, playerPort, false);

        var port = 3001;
        var middHost = new MiddlewareNetwork(isLocal ? "127.0.0.1" : GetLocalIPAddress(), port, true);

        middHost.AddInputHandler((i, s) => Console.WriteLine(string.Format("Host got message from {0}: {1}", i, s)));

        middHost.ConnectMiddleware(myMiddleware);
        middHost.ConnectMiddleware(playerMiddleware);
        myMiddleware.ConnectMiddleware(middHost);

        h = new Host(middHost, new List<IMiddleware> {
          myMiddleware,
          playerMiddleware,
        });

        GameState state = new GameState(
          new Player[] {
            new Player(
              0,
              new List<Card> { },
              new List<Card> { CardSet.SpawnCard(CardSet.CardName.MurlocTidehunter) },
              new List<Card> { CardSet.SpawnCard(CardSet.CardName.MurlocTidehunter), CardSet.SpawnCard(CardSet.CardName.GoldshireFootman), CardSet.SpawnCard(CardSet.CardName.MurlocScout) }
            ) ,
            new Player(
              1,
              new List<Card> { },
              new List<Card> { CardSet.SpawnCard(CardSet.CardName.FlashHeal), CardSet.SpawnCard(CardSet.CardName.FrostwolfWarlord) },
              new List<Card> { CardSet.SpawnCard(CardSet.CardName.IronforgeRifleman) }
            ) ,
          }
        );

        h.state = state;
      }
      else
      {
        var serverAddress = args[0];
        var arr = serverAddress.Split(":");
        var serverIp = arr[0];
        var serverPort = int.Parse(arr[1]);

        var port = 3002;
        myMiddleware = new MiddlewareNetwork(isLocal ? "127.0.0.1" : GetLocalIPAddress(), port, true);
        var serverMiddleware = new MiddlewareNetwork(serverIp, serverPort, false);

        myMiddleware.ConnectMiddleware(serverMiddleware);
        serverMiddleware.ConnectMiddleware(myMiddleware);
      }

      myMiddleware.AddInputHandler((i, s) => Console.WriteLine(string.Format("You got message from the host:\n{0}", s)));

      if (h != null)
      {
        Console.WriteLine("Press any key to start the game");
        Console.ReadKey();
        h.StartGame();
      }

      while (true)
      {
        var command = Console.ReadLine();
        myMiddleware.SendData(command);
      }
    }
  }
}
