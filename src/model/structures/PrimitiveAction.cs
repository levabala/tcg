using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  delegate GameState PrimitiveAction(GameState state, int[] args);
  delegate GameState SpecifiedAction(GameState state);
  delegate GameState SpecifiedAction<T1>(GameState state, int arg1);
  delegate GameState SpecifiedAction<T1, T2>(GameState state, int arg1, int arg2);
  delegate GameState SpecifiedAction<T1, T2, T3>(GameState state, int arg1, int arg2, int arg3);
  delegate GameState SpecifiedAction<T1, T2, T3, T4>(GameState state, int arg1, int arg2, int arg3, int arg4);
  delegate GameState SpecifiedAction<T1, T2, T3, T4, T5>(GameState state, int arg1, int arg2, int arg3, int arg4, int arg5);

}