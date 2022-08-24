using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionConstruction {
  class Program {
    static void Main(string[] args) {
      // Retrieve curried versions of Reduce and Sequence helpers

      var reduce = TypedReduce<int>();
      var curriedReduce = Curry(reduce);
      var sequence = TypedSequence<int>();
      var curriedSequence = Curry(sequence);

      // Partially apply Reduce to create a sum calculation function
      var sumCalculator = curriedReduce(
          (result, newval) => result + newval)(0);

      // In this case we can use a local function instead of the older-style lambda
      //Func<int, int> nextValueGenerator = cur => cur + 2;
      static int nextValueGenerator(int cur) => cur + 2;
      // Partially apply Sequence to generate series of odd numbers
      var oddNumbersSequence = curriedSequence(nextValueGenerator)(1);


      // To use this local function syntax for the curried format,
      // uncomment the Compose call below that has implicit generic parameters.
      // These parameters must be specified just once to make the compiler
      // happy. However, it's easier to stick to lambda syntax where
      // Compose works just like that...
      // static Func<int, bool> endChecker(int cutoff) =>
      //   val => nextValueGenerator(val) > cutoff;

      Func<int, Func<int, bool>> endChecker =
          cutoff => val => nextValueGenerator(val) > cutoff;

      // Compose the odd number sequence with a function that 
      // finds an end to the sequence
      // This is the version of the call with the implicit
      // generic parameters (mentioned above)
      // var oddNumbersSequenceFrom1ToX =
      //     Compose<int, Func<int, bool>, IEnumerable<int>>(endChecker, oddNumbersSequence);
      var oddNumbersSequenceFrom1ToX =
          Compose(endChecker, oddNumbersSequence);

      // Compose the limited sequence with the sum calculator
      var sumOfOddNumbers =
          Compose(oddNumbersSequenceFrom1ToX, sumCalculator);

      // Voila: a function that calculates the sum of odd numbers up to a given
      // cutoff value. Functional modularization.
      var sum = sumOfOddNumbers(10);
      Console.WriteLine(sum);
    }

    static IEnumerable<T> Sequence<T>(Func<T, T> getNext, T startVal, Func<T, bool> endReached) {
      if (getNext == null)
        yield break;
      yield return startVal;
      T val = startVal;
      while (endReached == null || !endReached(val)) {
        val = getNext(val);
        yield return val;
      }
    }

    static Func<TSource, TEndResult> Compose<TSource, TIntermediateResult, TEndResult>(
        Func<TSource, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2) {
      return sourceParam => func2(func1(sourceParam));
    }

    static IEnumerable<TResult> Map<TSource, TResult>(Converter<TSource, TResult> function,
        IEnumerable<TSource> list) {
      foreach (TSource sourceVal in list)
        yield return function(sourceVal);
    }

    static IEnumerable<T> Filter<T>(Predicate<T> predicate, IEnumerable<T> list) {
      foreach (T val in list)
        if (predicate(val))
          yield return val;
    }

    static TResult Reduce<TSource, TResult>(Func<TResult, TSource, TResult> accumulator, TResult startVal,
        IEnumerable<TSource> list) {
      TResult result = startVal;
      foreach (TSource sourceVal in list)
        result = accumulator(result, sourceVal);
      return result;
    }

    static Func<T1, Func<T2, T3>> Curry<T1, T2, T3>(Func<T1, T2, T3> func) {
      return par1 => par2 => func(par1, par2);
    }

    static Func<T1, Func<T2, Func<T3, T4>>> Curry<T1, T2, T3, T4>(Func<T1, T2, T3, T4> func) {
      return par1 => par2 => par3 => func(par1, par2, par3);
    }

    static Func<T1, Func<T2, Func<T3, Func<T4, T5>>>> Curry<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5> func) {
      return par1 => par2 => par3 => par4 => func(par1, par2, par3, par4);
    }

    static Func<Func<T, T, T>, T, IEnumerable<T>, T> TypedReduce<T>() {
      return Reduce<T, T>;
    }

    static Func<Func<T, T>, T, Func<T, bool>, IEnumerable<T>> TypedSequence<T>() {
      return Sequence<T>;
    }


  }
}

