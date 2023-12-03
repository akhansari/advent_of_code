module Day02

open System
open System.Text.RegularExpressions

let gameExp = Regex "Game (\d+):"
let redExp = Regex "(\d+) red"
let greenExp = Regex "(\d+) green"
let blueExp = Regex "(\d+) blue"
let getNums str (r: Regex) =
    r.Matches str |> Seq.map (_.Groups[1].Value >> Int32.Parse)

let runPartOne =
    Array.fold (fun sum line ->
        let lessThan exp max =
            getNums line exp |> Seq.forall (fun n -> n <= max)
        match
           lessThan redExp 12,
           lessThan greenExp 13,
           lessThan blueExp 14
        with
        | true, true, true -> Seq.head (getNums line gameExp) + sum
        | _ -> sum
        ) 0

let runPartTwo =
    Array.fold (fun sum line ->
       Seq.max   (getNums line redExp)
       * Seq.max (getNums line greenExp)
       * Seq.max (getNums line blueExp)
       + sum
       ) 0
