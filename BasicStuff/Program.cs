using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicStuff {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine(Closures()(30));

      //LazyEvaluation( );
    }

    static Func<int, int> Closures() {
      int baseVal = 10;

      Func<int, int> add = val => baseVal + val;
      Console.WriteLine(add(10));

      baseVal = 50;
      Console.WriteLine(add(10));

      return add;
    }

    static void LazyEvaluation() {
      AlternativeDoSomething(() => 0, GreatCalculation);
    }

    static int GreatCalculation() {
      Console.WriteLine("Executing GreatCalculation");

      // Do a really difficult and time-consuming calculation here
      return 42;
    }

    static void DoSomething(int a, int b) {
      Console.WriteLine("Executing DoSomething");

      // Sometimes we do something with b, but not all the time
      if (a != 0)
        Console.WriteLine(b);
    }

    static void AlternativeDoSomething(Func<int> a, Func<int> b) {
      Console.WriteLine("Executing DoSomething");

      // Sometimes we do something with b, but not all the time
      if (a() != 0)
        Console.WriteLine(b());
    }
  }
}
