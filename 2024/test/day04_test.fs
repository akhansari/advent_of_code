module Day04Tests

open Xunit
open Day04

let input =
    "MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX"

[<Fact>]
let ``test part 1`` () = input |> runPartOne =! 18

[<Fact>]
let ``test part 2`` () = input |> runPartTwo =! 9
