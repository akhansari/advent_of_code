module Day06Tests

open Xunit
open Day06

let input =
    "....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#..."

[<Fact>]
let ``test part 1`` () = input |> runPartOne =! 41

[<Fact>]
let ``test part 2`` () = input |> runPartTwo =! 6
