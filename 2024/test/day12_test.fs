module Day12Tests

open Xunit
open Day12

let inputSimple =
    "AAAA
BBCD
BBCC
EEEC"

let input =
    "RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE"

[<Fact>]
let ``test part 1 simple`` () =
    inputSimple |> runPartOne =! 140

[<Fact>]
let ``test part 1`` () =
    input |> runPartOne =! 1930

[<Fact>]
let ``test part 2 simple`` () =
    inputSimple |> runPartTwo =! 80

[<Fact>]
let ``test part 2`` () =
    input |> runPartTwo =! 1206

