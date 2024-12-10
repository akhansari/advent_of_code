module Day10Tests

open Xunit
open Day10

let input =
    "89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732"

[<Fact>]
let ``test part 1`` () =
    input |> runPartOne =! 36

[<Fact>]
let ``test part 2`` () =
    input |> runPartTwo =! 81
