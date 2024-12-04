module Day03Tests

open Xunit
open Day03

[<Fact>]
let ``test part 1`` () =
    "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
    |> runPartOne
    =! 161

[<Fact>]
let ``test part 2`` () =
    "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
    |> runPartTwo
    =! 48