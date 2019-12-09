// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net.Sockets;

// namespace tcg
// {
//   class MiddlewareNetwork : Middleware
//   {

//     public override void ConnectMiddleware(Middleware _middleware) {
//       if (_middleware is MiddlewareNetwork middleware) {

//       }
//       else
//         throw new ArgumentException("Provided middleware is not MiddlewareNetwork");
//     }

//     public override void  AddInputHandler(Action<int, string> handler) {

//     }

//     public override void SendData(string data) {

//     }

//     public override void SendDataPersonally(string data, int receiverIndex) {

//     }
//   }
// }