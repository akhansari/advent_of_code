module Day11Tests

open Xunit
open Day11

[<Fact>]
let ``test`` () =
    "125 17" |> runPartOne =! 55312

