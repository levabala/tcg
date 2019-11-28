using System;

namespace tcg
{
  class Program
  {
    static void Main(string[] args)
    {
     // Console.WriteLine("Hello World!");
          var card1 = new Card(10, 20, 5);
          var card2 = new Card(10, 20, 10);

          var player1 = new Player(1);
          var player2 = new Player(2);
          var players = new Player[] { player1, player2 };
          var gameState = new GameState(player1, players);
          var gameLoop = new GameLoop();

          gameState.Target = 0;
          gameState.Attacker = 0;
          gameState.CurrentPlayer = player1;

          player1.ActiveCards[0] = card1;
          player2.ActiveCards[0] = card2;
            
          while (!gameState.IsFinished)
          {
              var actionLine = Console.ReadLine();
              //convert to json
              var act = (ActionType)Enum.Parse(typeof(ActionType), actionLine); //should read from json
              
              var action = gameLoop.GenerateAction(gameState.CurrentPlayer.Id, act);

              ActionSet.actions[action.Type](gameState);

              Console.WriteLine(gameState.Players[0].ActiveCards[0]);
              Console.WriteLine(gameState.Players[1].ActiveCards[0]);
              gameState.IsFinished = true;
          } 
    }
  }
}
