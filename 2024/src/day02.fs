module Day02

open System

let parse = splitLines >> Array.map (split " " >> Array.map Int32.Parse)

let allDecreasing = Array.forall (fun (a, b) -> (b - a) >=< (-3, -1))
let allIncreasing = Array.forall (fun (a, b) -> (b - a) >=< (1, 3))

let isSafe nums =
    let pairs = Array.pairwise nums
    allDecreasing pairs || allIncreasing pairs

let count =
    function
    | true -> 1
    | false -> 0

let runPartOne input =
    parse input |> Array.sumBy (isSafe >> count)

let runPartTwo input =
    parse input
    |> Array.sumBy (fun nums ->
        if isSafe nums then
            1
        else
            [| 0 .. nums.Length - 1 |]
            |> Array.exists (fun i -> nums |> Array.removeAt i |> isSafe)
            |> count)
