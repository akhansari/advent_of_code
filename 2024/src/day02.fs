module Day02

open System

let parse = splitLines >> Array.map (split " " >> Array.map Int32.Parse)

// it's safe if the output map is an array of either 1 or -1
let isSafe report =
    Array.pairwise report
    |> Array.sumBy (fun (a, b) ->
        if b - a >=< (-3, -1) then -1
        elif b - a >=< (1, 3) then 1
        else 0)
    |> fun sum -> abs sum = report.Length - 1

let count =
    function
    | true -> 1
    | false -> 0

let runPartOne input =
    parse input |> Array.sumBy (isSafe >> count)

let runPartTwo input =
    parse input
    |> Array.sumBy (fun report ->
        if isSafe report then
            1
        else
            seq { 0 .. report.Length - 1 }
            |> Seq.exists (fun i -> report |> Array.removeAt i |> isSafe)
            |> count)
