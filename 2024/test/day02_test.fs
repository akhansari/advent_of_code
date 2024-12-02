module Day02Tests

open Xunit
open Day02

let input =
    "7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9"

[<Fact>]
let ``test part 1`` () = runPartOne input =! 2

[<Fact>]
let ``test part 2`` () = runPartTwo input =! 4
