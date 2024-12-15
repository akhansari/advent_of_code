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

    let bNumerator = py * ax - px * ay
    let bDenominator = by * ax - bx * ay

    if bNumerator % bDenominator <> 0 then
        None
    else
        let bPresses = bNumerator / bDenominator

        let aPressesNumerator = px - bPresses * bx
        let aPressesDenominator = ax

        if aPressesNumerator % aPressesDenominator <> 0 then
            None
        else
            let aPresses = aPressesNumerator / aPressesDenominator
            Some(aPresses * 3L + bPresses)

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
Linear Diophantine equation for integers
*)
