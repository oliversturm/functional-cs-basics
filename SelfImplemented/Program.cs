using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SelfImplemented {
  class Program {
    static void Main(string[] args) {
      var list = new int[] { 1, 2, 3, 4, 5 };

      OutputList("Original list", list);

      var mappedList = Map(val => val * val, list);

      OutputList("Mapped list", mappedList);

      var filteredList = Filter(val => val > 10, mappedList);

      OutputList("Filtered mapped list", filteredList);

      var overallSum = Reduce((sum, val) => sum + val, 0, mappedList);

      Console.WriteLine("Sum of mapped values: {0}", overallSum);


      //OutputWordCounts("hamlet", hamlet);
    }

    static IEnumerable<R> Map<T, R>(Converter<T, R> function,
        IEnumerable<T> list) {
      foreach (T sourceVal in list)
        yield return function(sourceVal);
    }

    static IEnumerable<T> Filter<T>(Predicate<T> predicate, IEnumerable<T> list) {
      foreach (T val in list)
        if (predicate(val))
          yield return val;
    }

    static R Reduce<T, R>(Func<R, T, R> accumulator, R startVal,
        IEnumerable<T> list) {
      R result = startVal;
      foreach (T sourceVal in list)
        result = accumulator(result, sourceVal);
      return result;
    }

    static IEnumerable<Group<TGroupKey, TData>> Group<TGroupKey, TData>(
        IEnumerable<TData> list, Converter<TData, TGroupKey> extractor) where TGroupKey : notnull {
      var dict = new Dictionary<TGroupKey, Group<TGroupKey, TData>>();
      foreach (TData data in list) {
        var extractedKey = extractor(data);
        if (!dict.ContainsKey(extractedKey))
          dict[extractedKey] = new Group<TGroupKey, TData>(extractedKey);
        dict[extractedKey].Add(data);
      }
      return dict.Values;
    }

    private static void OutputWordCounts(string title, string document) {
      var wordlist = document.Split(new[] { " ", Environment.NewLine },
          StringSplitOptions.RemoveEmptyEntries);

      var interimResult =
          Map(word => new KeyValuePair<string, int>(word, 1), wordlist);

      var groupedResult = Group(interimResult, pair => pair.Key);

      var result = new List<KeyValuePair<string, int>>();

      foreach (var group in groupedResult) {
        var resultPair = Reduce(
            (val1, val2) => new KeyValuePair<string, int>(val1.Key, val1.Value + val2.Value),
            new KeyValuePair<string, int>(group.Key, 0), group);
        result.Add(resultPair);
      }

      foreach (var pair in result)
        Console.WriteLine("Word: " + pair.Key + ", Count: " + pair.Value);
    }

    static void OutputList<T>(string listName, IEnumerable<T> list) {
      Console.Write("{0}: {{ ", listName);
      bool firstItem = true;
      foreach (var item in list) {
        if (firstItem)
          firstItem = false;
        else
          Console.Write(", ");
        Console.Write(item);
      }
      Console.WriteLine(" }");
    }

    const string hamlet = @"Though yet of Hamlet our dear brother's death
The memory be green, and that it us befitted
To bear our hearts in grief and our whole kingdom
To be contracted in one brow of woe,
Yet so far hath discretion fought with nature
That we with wisest sorrow think on him,
Together with remembrance of ourselves.
Therefore our sometime sister, now our queen,
The imperial jointress to this warlike state,
Have we, as 'twere with a defeated joy,--
With an auspicious and a dropping eye,
With mirth in funeral and with dirge in marriage,
In equal scale weighing delight and dole,--
Taken to wife: nor have we herein barr'd
Your better wisdoms, which have freely gone
With this affair along. For all, our thanks.
Now follows, that you know, young Fortinbras,
Holding a weak supposal of our worth,
Or thinking by our late dear brother's death
Our state to be disjoint and out of frame,
Colleagued with the dream of his advantage,
He hath not fail'd to pester us with message,
Importing the surrender of those lands
Lost by his father, with all bonds of law,
To our most valiant brother. So much for him.
Now for ourself and for this time of meeting:
Thus much the business is: we have here writ
To Norway, uncle of young Fortinbras,--
Who, impotent and bed-rid, scarcely hears
Of this his nephew's purpose,--to suppress
His further gait herein; in that the levies,
The lists and full proportions, are all made
Out of his subject: and we here dispatch
You, good Cornelius, and you, Voltimand,
For bearers of this greeting to old Norway;
Giving to you no further personal power
To business with the king, more than the scope
Of these delated articles allow.
Farewell, and let your haste commend your duty.";


  }
}