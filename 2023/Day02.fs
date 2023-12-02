module Day02

open System
open System.Text.RegularExpressions

let gameExp = Regex "Game (\d+):"
let redExp = Regex "(\d+) red"
let greenExp = Regex "(\d+) green"
let blueExp = Regex "(\d+) blue"
let getNums (r: Regex) str =
    r.Matches str |> Seq.map (fun m -> Int32.Parse m.Groups[1].Value)

let runPartOne =
    Array.fold (fun sum line ->
        match
           getNums redExp line |> Seq.forall (fun n -> n <= 12),
           getNums greenExp line |> Seq.forall (fun n -> n <= 13),
           getNums blueExp line |> Seq.forall (fun n -> n <= 14)
           with
        | true, true, true -> Seq.head (getNums gameExp line) + sum
        | _ -> sum
        ) 0

let run =
    Array.fold (fun sum line ->
       Seq.max (getNums redExp line)
       * Seq.max (getNums greenExp line)
       * Seq.max (getNums blueExp line)
       + sum
       ) 0
