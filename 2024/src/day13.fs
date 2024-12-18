module Day13

open System.Text.RegularExpressions

type Machine =
    { ButtonA: int64 * int64
      ButtonB: int64 * int64
      Prize: int64 * int64 }

let parse input =
    let pattern = Regex "X[=+](?<x>\d+), Y[=+](?<y>\d+)"
    input
    |> split "\n\n"
    |> Array.map (fun block ->
        let lines = block |> split "\n" |> Array.map (fun line ->
            let m = pattern.Match line
            (int64 m.Groups["x"].Value, int64 m.Groups["y"].Value))
        { ButtonA = lines[0];
          ButtonB = lines[1];
          Prize = lines[2] })

let minTokens machine =
    let ax, ay = machine.ButtonA
    let bx, by = machine.ButtonB
    let px, py = machine.Prize

    match (px * by - py * bx), (ax * by - ay * bx) with
    | tan, tad when tan % tad = 0L ->
        let ta = tan / tad
        match (px - ta * ax), bx with
        | tbn, tbd when tbn % tbd = 0L ->
            let tb = tbn / tbd
            Some (ta * 3L + tb)
        | _ ->
            None
    | _ ->
        None

let sumTokens map machines =
    machines |> Array.sumBy (fun m ->
        match m |> map |> minTokens with
        | Some tokens -> tokens
        | None -> 0)

let runPartOne input =
    parse input |> sumTokens id

let runPartTwo input =
    let error = 10000000000000L
    parse input
    |> sumTokens (fun m ->
        { m with Prize = (fst m.Prize + error, snd m.Prize + error) })

(*
if button A to prize and button B to prize are not parallel lines, they can cross only once.
    So there is only one solution and it dosen't make sens to calculate a minimum number of tokens.

px = (ta * ax) + (tb * bx)
py = (ta * ay) + (tb * by)

multiply by 'bx' and 'by'

px * by = (ta * ax * by) + (tb * bx * by)
py * bx = (ta * ay * bx) + (tb * by * by)

substract both equations

(px * by) - (py * bx) = (ta * ax * by) - (ta * ay * bx)
(px * by) - (py * bx) = ta * ((ax * by) - (ay * bx))

ta = (px * by - py * bx) / (ax * by - ay * bx)

(ax * by - ay * bx) could never be zero because it means that button A and button B are parallel.
now that we have ta, we can calculate tb.

tb = (px - ta * ax) / bx

finally, we need to check that ta and tb are integers.
*)
