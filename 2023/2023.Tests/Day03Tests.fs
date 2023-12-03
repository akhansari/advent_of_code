module Day03.Tests

open Xunit

let sample =
    "467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."

[<Fact>]
let ``Part one`` () =
    4361 =! (splitLines sample |> runPartOne)

[<Fact>]
let ``Part two`` () =
    467835 =! (splitLines sample |> runPartTwo)

[<Fact>]
let ``Part two, tricky combination`` () =
    let str = "12..2*2..*..
+.........34
.......-12..
..78........
..*....60...
78.........9
.5.....23..$
8...90*12...
............
2.2......12.
.*.........*
1.1..503+.56"
    6760 =! (splitLines str |> runPartTwo)