module Day01Tests

open Xunit

let input =
    "3   4
4   3
2   5
1   3
3   9
3   3"

[<Fact>]
let ``test day 01 part 1`` () = Day01.runPartOne input =! 11

[<Fact>]
let ``test day 01 part 2`` () = Day01.runPartTwo input =! 31
