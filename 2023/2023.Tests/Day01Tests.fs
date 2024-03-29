module Day01.Tests

open Xunit

[<Fact>]
let ``Part two`` () =
    let sample =
        "two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen"
    281 =! (splitLines sample |> runPartTwo)
    
[<Fact>]
let ``Tricky combination`` () =
    79 =! (runPartTwo [| "sevenine" |])
    