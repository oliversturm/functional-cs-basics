namespace ResultMonadDemo;

using static ResultModule;

public abstract record Error {
  public sealed record Maths(string Message) : Error;
}

internal class Program {
  private static Result<int, Error> Divide(int a, int b) {
    return b != 0 ? Ok(a / b) : Fail(new Error.Maths("Division by zero"));
  }

  private static Result<int, Error> Add(int a, int b) => Ok(a + b);
  private static Result<int, Error> Subtract(int a, int b) => Ok(a - b);
  private static Result<int, Error> Multiply(int a, int b) => Ok(a * b);

  private static void Process(int start) =>
    Add(start, start)
      //.Log("add", "Result")
      .Bind(x => Subtract(x, 28))
      //.Log("subtract", "Result")
      .Bind(x => Multiply(x, 3))
      //.Log("multiply", "Result")
      .Bind(x => Divide(100, x))
      .Switch(
        x => Console.WriteLine($"Result: {x}"),
        e => Console.WriteLine($"Error: {e}")
      );

  private static void Main(string[] args) {
    Process(21);
    Process(14);
  }
}