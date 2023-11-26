Console.WriteLine("New record-specific 'with' mechanism");

var oli = new Person("Oli", "Sturm", 27);

Console.WriteLine(oli);

// "with" can use records, and since C# also structs or anonymous types
var nextYearOli = oli with { Age = oli.Age + 1 };

Console.WriteLine($"Old Oli: {oli}");
Console.WriteLine($"Older Oli: {nextYearOli}");

public record Person(string FirstName, string Name, int Age);

