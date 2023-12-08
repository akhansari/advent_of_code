module Day08.Tests

open Xunit

[<Fact>]
let ``Part one, one go`` () =
    let sample =
        "RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)"
    2 =! (splitLines sample |> runPartOne)

[<Fact>]
let ``Part one, loop`` () =
    let sample =
        "LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)"
    6 =! (splitLines sample |> runPartOne)

[<Fact>]
let ``Part two`` () =
    let sample =
        "LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)"
    6L =! (splitLines sample |> runPartTwo)