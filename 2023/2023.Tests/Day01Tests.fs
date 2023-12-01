module Day01.Tests

open Xunit

[<Fact>]
let Sample () =
    let sample =
        "two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen"
    Assert.Equal(281, split sample |> run)
    
[<Fact>]
let Tricky () =
    Assert.Equal(79, run [| "sevenine" |])
    