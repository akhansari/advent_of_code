module Day01

open System
open System.Text.RegularExpressions

let digitsStr =
    [| "one", 1; "two", 2; "three", 3; "four", 4; "five", 5
       "six", 6; "seven", 7; "eight", 8; "nine", 9 |]
    |> Map
let [<Literal>] pattern = "(one|two|three|four|five|six|seven|eight|nine|[1-9])"
let firstExp = Regex(pattern, RegexOptions.Compiled)
let lastExp = Regex(pattern, RegexOptions.Compiled ||| RegexOptions.RightToLeft)
let toDigit (m: Match) =
    if m.Value.Length = 1
    then Int32.Parse m.Value
    else digitsStr[m.Value]
let findFirst = firstExp.Match >> toDigit
let findLast = lastExp.Match >> toDigit

let run =
    Array.sumBy (fun line ->
        findFirst line * 10 + findLast line)
