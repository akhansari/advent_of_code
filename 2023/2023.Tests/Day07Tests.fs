module Day07.Tests

open Xunit

let sample =
    "32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483"

let sampleTricky =
    "2345A 1
Q2KJJ 13
Q2Q2Q 19
T3T3J 17
T3Q33 11
2345J 3
J345A 2
32T3K 5
T55J5 29
KK677 7
KTJJT 34
QQQJA 31
JJJJJ 37
JAAAA 43
AAAAJ 59
AAAAA 61
2AAAA 23
2JJJJ 53
JJJJ2 41"

[<Fact>]
let ``Part one`` () =
    6440 =! (splitLines sample |> runPartOne)

[<Fact>]
let ``Part one, tricky`` () =
    6592 =! (splitLines sampleTricky |> runPartOne)

[<Fact>]
let ``Part two`` () =
    5905 =! (splitLines sample |> runPartTwo)

[<Fact>]
let ``Part two, tricky`` () =
    6839 =! (splitLines sampleTricky |> runPartTwo)
