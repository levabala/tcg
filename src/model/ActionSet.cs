using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
    
    class ActionSet
    {
        public static Dictionary<ActionType, Action<GameState>> actions = new Dictionary<ActionType, Action<GameState>>
        {
            {ActionType.Attack, ActionSet.Attack },
        };  

        private static void Attack(GameState GS)
        {
            var attacker = GS.CurrentPlayer;
            var target = GS.Players[0].Id != attacker.Id ? GS.Players[0] : GS.Players[1];

            var attackerCard = attacker.ActiveCards[GS.Attacker];
            var targetCard = target.ActiveCards[GS.Target]; 

            var newAttackerHP = attackerCard.HP - targetCard.Attack;
            var newTargetHP = targetCard.HP - attackerCard.Attack;

            attackerCard.HP -= targetCard.Attack;
            targetCard.HP -= attackerCard.Attack;

            attacker.ActiveCards[0] = new Card(attackerCard.ManaCost, newAttackerHP, attackerCard.Attack);
            target.ActiveCards[0] = new Card(targetCard.ManaCost, newTargetHP, targetCard.Attack);
        }  
    }
}
