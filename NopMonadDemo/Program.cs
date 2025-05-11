// Give the Monad a value to encapsulate
var m1 = new NopMonad<int>(10);

// Let's chain some Bind operations
var m2 =
  m1.
    Bind(i => new NopMonad<int>(i * 2)).
    Bind(i => new NopMonad<int>(i + 5));

// Print out the result
Console.WriteLine(m2.Value);


// This Monad implementation does nothing other than fulfill
// the Monad "contract". It encapsulates a value and offers
// a Bind function which applies a new operation to the
// encapsulated value and then returns a new Monad instance.
public class NopMonad<T>(T value) {
  public T Value { get; } = value;

  public NopMonad<T> Bind(Func<T, NopMonad<T>> g) {
    return g(Value);
  }
}
