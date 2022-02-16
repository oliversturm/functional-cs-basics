Console.WriteLine("New record-specific 'with' mechanism");

var oli = new Person("Oli", "Sturm", 27);

Console.WriteLine(oli);

var nextYearOli = oli with { Age = oli.Age + 1 };

Console.WriteLine(nextYearOli);


// Side note: what on earth is the reason that C# requires me to include this
// type declaration **after** its use in the top-level statements above?!??!
// The blue squigglies indicate that I should "declare types in namespaces".

public record Person(string FirstName, string Name, int Age);
