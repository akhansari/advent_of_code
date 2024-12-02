module Day01Tests

open Xunit
open Day01

let input =
    "3   4
4   3
2   5
1   3
3   9
3   3"

[<Fact>]
let ``test part 1`` () = runPartOne input =! 11

[<Fact>]
let ``test part 2`` () = runPartTwo input =! 31
