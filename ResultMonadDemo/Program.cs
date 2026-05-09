namespace ResultMonadDemo;

using static ResultModule;

public abstract record Error {
  public sealed record Maths(string Message) : Error;
}

internal class Program {
  private static Result<int, Error.Maths> Divide(int a, int b) {
    return b != 0 ? Ok(a / b) : Fail(new Error.Maths("Division by zero"));
  }

  private static Result<int, Error.Maths> Add(int a, int b) => Ok(a + b);
  private static Result<int, Error.Maths> Subtract(int a, int b) => Ok(a - b);
  private static Result<int, Error.Maths> Multiply(int a, int b) => Ok(a * b);

  private static void Process(int start) =>
    Add(start, start)
      .Bind(x => Subtract(x, 27))
      .Bind(x => Multiply(x, 3))
      .Bind(x => Divide(100, x))
      .Switch(
        x => Console.WriteLine($"Result: {x}"),
        e => Console.WriteLine($"Error: {e}")
      );

  private static void Main(string[] args) {
    Process(21);
  }
}