using System;
using System.Threading;

namespace tcg
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Start");

      var port1 = int.Parse(args[0]);
      var mid1 = new MiddlewareNetwork("127.0.0.1", port1, true);

      var port2 = int.Parse(args[1]);
      var mid2 = new MiddlewareNetwork("127.0.0.1", port2, true);

      mid1.ConnectMiddleware(mid2);
      mid2.ConnectMiddleware(mid1);

      mid1.AddInputHandler((i, s) => Console.WriteLine(string.Format("Mid1 got message from {0}: {1}", i, s)));
      mid2.AddInputHandler((i, s) => Console.WriteLine(string.Format("Mid2 got message from {0}: {1}", i, s)));

      while (true)
      {
        Console.WriteLine("--- New sending ---");
        mid1.SendData("message from mid1");
        mid2.SendData("message from mid2");
        Console.WriteLine("Sending done");
        Console.ReadKey();
      }

      // Console.WriteLine("Sending done");
    }
  }
}
