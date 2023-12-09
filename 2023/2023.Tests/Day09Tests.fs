module Day09.Tests

open Xunit

let sample =
    "0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45"

[<Fact>]
let ``Part one`` () =
    114 =! (splitLines sample |> runPartOne)

[<Fact>]
let ``Part two`` () =
    2 =! (splitLines sample |> runPartTwo)
