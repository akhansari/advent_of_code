module Day14.Tests

open Xunit

let sample =
    "O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#...."

[<Fact>]
let ``Part one`` () =
    136 =! (sample |> runPartOne)

[<Fact>]
let ``Part two, spin cycle`` () =
    let grid = sample |> parse
    spinCycle grid
    ".....#....
....#...O#
...OO##...
.OO#......
.....OOO#.
.O#...O#.#
....O#....
......OOOO
#...O###..
#..OO#...."
    =! (Array2D.toString grid)

[<Fact>]
let ``Part two`` () =
    64 =! (sample |> runPartTwo)
