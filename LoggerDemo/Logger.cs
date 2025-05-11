using System;
using System.Text;
using FCSColl = FCSlib.Data.Collections;

namespace LoggerDemo;

// The Logger is a Monad. Encapsulation, Bind, all there.
public class Logger<T>(T val, FCSColl::List<string> outputLines)
{
  private readonly FCSColl::List<string> outputLines = outputLines;

  public T Value
  {
    get { return val; }
  }

  public Logger(T val, string message)
    : this(val, new FCSColl::List<string>(message)) { }

  public Logger(T val)
    : this(val, FCSColl::List<string>.Empty) { }

  public string ChainOutput()
  {
    var builder = new StringBuilder();
    foreach (string outputLine in outputLines)
      builder.AppendLine(outputLine);
    return builder.ToString();
  }

  // Bind chains a new operation, but it also does something completely
  // unrelated by collecting all output lines.
  public Logger<R> Bind<R>(Func<T, Logger<R>> g)
  {
    var r = g(val);
    return new Logger<R>(r.Value, outputLines.Append(r.outputLines));
  }

  // Alias - usability in the intended use cases!
  public Logger<R> Step<R>(Func<T, Logger<R>> g) => Bind(g);
}

// These helpers are meant to provide an API that a developer would find
// a bit more intuitive that the "native" Monad semantics.
public static class LoggerHelpers
{
  public static Logger<T> BeginProcessingChain<T>(this T val) => new(val);

  public static Logger<T> BeginProcessingChain<T>(this T val, string message) => new(val, message);

  public static Logger<T> BeginProcessingChain<T>(
    this T val,
    string format,
    params object[] args
  ) => new(val, String.Format(format, args));

  // Aliases
  public static Logger<T> StepOutput<T>(this T val, string message) =>
    BeginProcessingChain(val, message);

  public static Logger<T> StepOutput<T>(this T val, string message, params object[] args) =>
    BeginProcessingChain(val, message, args);
}
