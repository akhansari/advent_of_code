module Day06

open System.Collections.Generic

let parse input =
    let arr = input |> splitLines |> Array.map Array.ofSeq |> array2D
    let rows = arr.GetLength 0
    let cols = arr.GetLength 1

    let start, obstructions =
        (((0, 0), Set.empty), arr)
        ||> Array2D.foldi (fun x y c (start, obstructions) ->
            match c with
            | '#' -> (start, Set.add (x, y) obstructions)
            | '^' -> ((x, y), obstructions)
            | _ -> (start, obstructions))

    {| IsInBound = fun (x, y) -> x >= 0 && y >= 0 && x < rows && y < cols
       Start = start
       Obstructions = obstructions |}

type Direction =
    | Left
    | Right
    | Up
    | Down

let nextPos (x, y) =
    function
    | Right -> (x, y + 1)
    | Left -> (x, y - 1)
    | Down -> (x + 1, y)
    | Up -> (x - 1, y)

let turnRight =
    function
    | Up -> Right
    | Right -> Down
    | Down -> Left
    | Left -> Up

type Reason =
    | OutOfBound
    | Loop

let patrol isInBound startPos startDir obstructions =
    let trace = HashSet()

    let rec patrol pos dir reason =
        let next = nextPos pos dir
        let nextIsBlocked = Set.contains next obstructions
        let isLoop = trace.Contains(pos, dir)

        match isLoop, isInBound next, nextIsBlocked with
        | true, _, _ -> Loop
        | false, true, true -> patrol pos (turnRight dir) reason
        | false, true, false ->
            (pos, dir) |> trace.Add |> ignore
            patrol next dir reason
        | false, false, _ ->
            (pos, dir) |> trace.Add |> ignore
            OutOfBound

    let reason = patrol startPos startDir OutOfBound
    reason, Array.ofSeq trace

let getDefaultPath isInBound start obstructions =
    match patrol isInBound start Up obstructions with
    | OutOfBound, trace -> trace |> Array.map fst |> Array.distinct
    | _ -> failwith "Should not happen"

let runPartOne input =
    let data = parse input

    getDefaultPath data.IsInBound data.Start data.Obstructions |> Array.length

let runPartTwo input =
    let data = parse input
    let loops = HashSet()
    let defaultPath = getDefaultPath data.IsInBound data.Start data.Obstructions

    for pos in defaultPath do
        Set.add pos data.Obstructions
        |> patrol data.IsInBound data.Start Up
        |> function
            | Loop, _ -> pos |> loops.Add |> ignore
            | _ -> ()

    loops.Count
