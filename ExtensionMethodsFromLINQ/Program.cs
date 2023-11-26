using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethodsFromLINQ {
  class Program {

    static void Main(string[] args) {

      OutputWordCounts("hamlet", hamlet);

    }

    private static void OutputWordCounts(string title, string document) {
      var wordlist = document.Split(new[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

      var results = wordlist.Select(key => new { Key = key, Value = 1 }).
          GroupBy(pair => pair.Key).
          Select(group =>
              group.Aggregate((group1, group2) =>
                  new { Key = group1.Key, Value = group1.Value + group2.Value }));

      foreach (var pair in results)
        Console.WriteLine("Word: " + pair.Key + ", Count: " + pair.Value);
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
