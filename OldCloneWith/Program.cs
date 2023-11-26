using OldCloneWith;

Action<Person> outputPerson = p => {
  Console.WriteLine($"Person {p.FirstName} {p.Name}, Age {p.Age}");
};

Console.WriteLine("Old CloneWith helper");

var oli = new Person("Oli", "Sturm", 27);

outputPerson(oli);

var nextYearOli = oli.CloneWith(new Dictionary<string, object> {
  { "Age", oli.Age + 1 }
});

outputPerson(nextYearOli);
