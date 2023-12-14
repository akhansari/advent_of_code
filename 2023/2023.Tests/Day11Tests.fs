module Day11.Tests

open Xunit

let sample =
    "...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#....."

[<Fact>]
let ``Part one`` () =
    374L =! (splitLines sample |> runPartOne)

[<Fact>]
let ``Part two, 10 times`` () =
    1030L =! (splitLines sample |> parse |> run 10)

[<Fact>]
let ``Part two, 100 times`` () =
    8410L =! (splitLines sample |> parse |> run 100)
