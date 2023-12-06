module Day06.Tests

open Xunit

let sample =
    "Time:      7  15   30
Distance:  9  40  200"

[<Fact>]
let ``Part one`` () =
    288 =! (splitLines sample |> runPartOne)

[<Fact>]
let ``Part two`` () =
    71503L =! (splitLines sample |> runPartTwo)

