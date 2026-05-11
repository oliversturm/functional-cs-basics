// This first function uses the same steps as the C# example
let sumOfOddNumbersDemoLikeCS =
    // No Curry() helper - every F# function is curried by default.
    // No TypedReduce<int>() - generic inference handles it from usage.
    // No Compose() helper - forward composition is the >> operator.

    // Sum calculator: Seq.fold partially applied to (+) and 0.
    let sumCalculator = Seq.fold (+) 0

    // Sequence builder: generate values starting from `start`,
    // applying `next`, stopping when `endCheck` returns true.
    let sequence next start endCheck =
        Seq.unfold
            (function
            | None -> None
            | Some n -> Some(n, if endCheck n then None else Some(next n)))
            (Some start)

    let nextValueGenerator cur = cur + 2

    // Partial application: supply the first two parameters,
    // leaving endCheck open. No Curry() needed - just omit arguments.
    let oddNumbers = sequence nextValueGenerator 1

    // Given a cutoff, returns a stop predicate.
    let endChecker cutoff value = nextValueGenerator value > cutoff

    // Forward composition - built-in operator.
    let oddNumbersUpTo = endChecker >> oddNumbers
    let sumOfOddNumbersUpTo = oddNumbersUpTo >> sumCalculator

    printfn $"%d{sumOfOddNumbersUpTo 10}"


// This second function still uses the same steps, more or less,
// but in a more F#-idiomatic way
let sumOfOddNumbersDemoMoreFsharpish =
    (fun c n -> n + 2 > c)
    >> (fun stop ->
        Seq.unfold
            (function
            | None -> None
            | Some n -> Some(n, if stop n then None else Some(n + 2)))
            (Some 1))
    >> Seq.sum

// the really short version in F# - without demonstrating the original
// points about function construction
let sumOfOddNumbersDemoReallyFsharpish n = [ 1..2..n ] |> List.sum

// of course C# could also do this:
// int ReallyShortInCS(int n) =>
//     Enumerable.Range(1, 10).Where(n => n % 2 == 1).Sum();

[<EntryPoint>]
let main _ =
    sumOfOddNumbersDemoLikeCS
    printfn $"%d{sumOfOddNumbersDemoMoreFsharpish 10}"
    printfn $"%d{sumOfOddNumbersDemoReallyFsharpish 10}"

    0
