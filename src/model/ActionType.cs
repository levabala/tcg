﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcg
{
  public enum ActionType
  {
    Attack,
    Heal,
    DrawCard,
    PlayCard,
    ProcessDeath,
    DealDamage,
    EndTurn,
    WakeUpCreatures,
    PerformActionOnRandomCard,
    BuffCreature,
    SaveChanges
  }
}
