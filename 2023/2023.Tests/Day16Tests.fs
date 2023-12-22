module Day16.Tests

open Xunit

let sample =
    @".|...\....
|.-.\.....
.....|-...
........|.
..........
.........\
..../.\\..
.-.-/..|..
.|....-|.\
..//.|...."

[<Fact>]
let ``Part one`` () =
    46 =! (sample |> runPartOne)

[<Fact>]
let ``Part two`` () =
    51 =! (sample |> runPartTwo)
