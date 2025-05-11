using FCSlib;
using LoggerDemo;

List<Order> orders =
[
  new(Date: new DateTime(2024, 6, 3), Value: 29.9m),
  new(Date: new DateTime(2024, 6, 3), Value: 18.6m),
  new(Date: new DateTime(2024, 6, 4), Value: 119.99m),
  new(Date: new DateTime(2024, 7, 1), Value: 3.99m),
  new(Date: new DateTime(2024, 7, 2), Value: 47.62m),
  new(Date: new DateTime(2024, 7, 3), Value: 99.99m),
];

// Using the aliased monadic API, we can now step through a processing chain,
// providing output for each step, which is accumulated as a secondary concern
// and can be retrieved later or processed further. This is similar
// to the Haskell "do" notation, which relies on the IO monad -- only C#
// doesn't have a handy "do" notation.
var average = orders
  .BeginProcessingChain("Starting with a list of {0} orders", orders.Count())
  .Step(l =>
    Functional
      .Filter(o => o.Date >= new DateTime(2024, 7, 1), l)
      .StepOutput("Got list with {0} items, filtering...", l.Count())
  )
  .Step(l =>
    l.Average(o => o.Value)
      .StepOutput("Calculating average for list with remaining {0} items...", l.Count())
  );

Console.WriteLine("Result: " + average.Value);

Console.WriteLine("-------- Log Output:");
Console.Write(average.ChainOutput());

public record Order(DateTime Date, decimal Value);
