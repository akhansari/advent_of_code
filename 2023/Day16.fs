module Day16

open System.Collections.Generic

type Direction = West | East | North | South | Stop
type BeamPath =
    | Take  of Direction
    | Split of Direction * Direction

let nextPositionOf (x,y) = function
    | West  -> x,   y-1
    | East  -> x,   y+1
    | North -> x-1, y
    | South -> x+1, y
    | Stop  -> x,   y

let nextDirectionOf contraption dir =
    match contraption, dir with
    | '/',  North -> Take East
    | '/',  West  -> Take South
    | '/',  South -> Take West
    | '/',  East  -> Take North
    | '\\', North -> Take West
    | '\\', West  -> Take North
    | '\\', South -> Take East
    | '\\', East  -> Take South
    | '-', (North | South) -> Split (West,  East)
    | '|', (West  | East)  -> Split (North, South)
    | _,   dir    -> Take dir

let parse content =
    content |> splitLines |> Array.map Array.ofSeq |> array2D

let energize grid startDir startPos =
    let visited = HashSet()

    let rec go dir (x,y) =
        if Array2D.inBound (x,y) grid && visited.Add (x,y,dir) then
            match nextDirectionOf grid[x,y] dir with
            | Take nd ->
                nextPositionOf (x,y) nd |> go nd
            | Split (nd1, nd2) ->
                nextPositionOf (x,y) nd1 |> go nd1
                nextPositionOf (x,y) nd2 |> go nd2

    go startDir startPos

    visited
    |> Seq.distinctBy (fun (x, y, _) -> x, y)
    |> Seq.length

let runPartOne content =
    let grid = parse content
    energize grid East (0,0)

let runPartTwo (content: string) =
    let grid = parse content
    let rows = Array2D.length1 grid
    let cols = Array2D.length2 grid
    seq {
        for x in 0..rows-1 do
            energize grid East (x,0)
            energize grid West (x,cols-1)
        for y in 0..cols-1 do
            energize grid South (0,y)
            energize grid North (rows-1,y)
    }
    |> Seq.max
