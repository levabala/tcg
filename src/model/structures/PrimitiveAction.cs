using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
  delegate GameState PrimitiveAction(GameState state, int[] args);
  delegate GameState SpecifiedAction(GameState state, int[] remainArguments);
  delegate GameState SpecifiedAction<T1>(GameState state, int arg1, int[] remainArguments);
  delegate GameState SpecifiedAction<T1, T2>(GameState state, int arg1, int arg2, int[] remainArguments);
  delegate GameState SpecifiedAction<T1, T2, T3>(GameState state, int arg1, int arg2, int arg3, int[] remainArguments);
  delegate GameState SpecifiedAction<T1, T2, T3, T4>(GameState state, int arg1, int arg2, int arg3, int arg4, int[] remainArguments);
  delegate GameState SpecifiedAction<T1, T2, T3, T4, T5>(GameState state, int arg1, int arg2, int arg3, int arg4, int arg5, int[] remainArguments);
  delegate GameState SpecifiedAction<T1, T2, T3, T4, T5, T6>(GameState state, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int[] remainArguments);
  delegate GameState SpecifiedAction<T1, T2, T3, T4, T5, T6, T7>(GameState state, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7, int[] remainArguments);
  delegate GameState SpecifiedAction<T1, T2, T3, T4, T5, T6, T7, T8>(GameState state, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7, int arg8, int[] remainArguments);
  delegate GameState SpecifiedAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(GameState state, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7, int arg8, int arg9, int[] remainArguments);

}