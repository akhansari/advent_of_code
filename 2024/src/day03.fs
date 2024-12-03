module Day03

open System.Text.RegularExpressions

let runPartOne input =
    Regex.Matches(input, "mul\((\d+),(\d+)\)")
    |> Seq.sumBy (fun m -> int m.Groups[1].Value * int m.Groups[2].Value)

let runPartTwo input =
    ((0, true), Regex.Matches(input, "(?<do>do\(\))|(?<dont>don't\(\))|mul\((?<a>\d+),(?<b>\d+)\)"))
    ||> Seq.fold (fun (sum, todo) m ->
        match (m.Groups["do"].Success, m.Groups["dont"].Success, todo) with
        | true, _, _ -> (sum, true)
        | _, true, _ -> (sum, false)
        | _, _, true -> (sum + int m.Groups["a"].Value * int m.Groups["b"].Value, true)
        | _ -> (sum, todo))
    |> fst
