module Day00Tests

open Xunit
open Day00

let input =
    ""

[<Fact>]
let ``test part 1`` () =
    input |> runPartOne =! 0

[<Fact>]
let ``test part 2`` () =
    input |> runPartTwo =! 0
