using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManualCurrying {
  class Program {
    static void Main(string[] args) {
      DelegateImplementation();
      //LambdaImplementation();
    }

    private static void DelegateImplementation() {
      Func<int, int, int> add =
          delegate (int x, int y) {
            return x + y;
          };

      Console.WriteLine(add(10, 20));

      Func<int, Func<int, int>> curriedAdd =
          delegate (int x) {
            return delegate (int y) {
              return add(x, y);
            };
          };

      Console.WriteLine(curriedAdd(15)(5));

      Func<int, int> add5 = curriedAdd(5);
      Console.WriteLine(add5(27));
    }

    private static void LambdaImplementation() {
      Func<int, int, int> add = (x, y) => x + y;
      Console.WriteLine(add(10, 20));

      Func<int, Func<int, int>> curriedAdd =
          x => y => add(x, y);
      Console.WriteLine(curriedAdd(15)(5));

      Func<int, int> add5 = curriedAdd(5);
      Console.WriteLine(add5(27));
    }
  }
}
