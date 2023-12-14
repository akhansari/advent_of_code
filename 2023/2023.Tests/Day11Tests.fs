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
    374L =! (sample |> runPartOne)

[<Fact>]
let ``Part two, 10 times`` () =
    1030L =! (sample |> parse |> run 10)

[<Fact>]
let ``Part two, 100 times`` () =
    8410L =! (sample |> parse |> run 100)
