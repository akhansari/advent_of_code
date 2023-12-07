module Day02

open System.Text.RegularExpressions

let gameExp = Regex @"Game (\d+):"
let redExp = Regex @"(\d+) red"
let greenExp = Regex @"(\d+) green"
let blueExp = Regex @"(\d+) blue"
let getNums str (r: Regex) =
    r.Matches str |> Seq.map (_.Groups[1].Value >> int32)

let runPartOne =
    Array.sumBy (fun line ->
        let lessThan max exp =
            getNums line exp |> Seq.forall (fun n -> n <= max)
        match
            lessThan 12 redExp,
            lessThan 13 greenExp,
            lessThan 14 blueExp
        with
        | true, true, true ->
            getNums line gameExp |> Seq.head
        | _ ->
            0)

let runPartTwo =
    Array.sumBy (fun line ->
       Seq.max   (getNums line redExp)
       * Seq.max (getNums line greenExp)
       * Seq.max (getNums line blueExp))
