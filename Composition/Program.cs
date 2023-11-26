using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Composition {
  class Program {
    static void Main(string[] args) {
      Func<int, int> add5 = val => val + 5;
      Func<int, int> triple = val => val * 3;

      var add5AndTriple = Compose(add5, triple);
      var tripleAndAdd5 = Compose(triple, add5);
      Console.WriteLine(add5AndTriple(10));
      Console.WriteLine(tripleAndAdd5(10));
    }

    static Func<TSource, TEndResult> Compose<TSource, TIntermediateResult, TEndResult>(
        Func<TSource, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2) {
      return sourceParam => func2(func1(sourceParam));
    }

  }
}
