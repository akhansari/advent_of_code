module Day14

open System.Collections.Generic

type Direction = West | East | North | South

let nextPos (x,y) = function
    | West  -> x,   y-1
    | East  -> x,   y+1
    | North -> x-1, y
    | South -> x+1, y

let parse content =
    content |> splitLines |> Array.map Array.ofSeq |> array2D

let roll direction (grid: char[,]) =
    let rows = Array2D.length1 grid
    let cols = Array2D.length2 grid

    let rec farthestPoint (x,y) =
        let nx,ny = nextPos (x,y) direction
        if Array2D.inBound (nx,ny) grid then
            if grid[nx,ny] = '.'
            then farthestPoint (nx,ny)
            else x,y
        else
            x,y

    let switch x y =
        if grid[x,y] = 'O' then
            let nx,ny = farthestPoint (x,y)
            grid[x,y] <- '.'
            grid[nx,ny] <- 'O'

    match direction with
    | North ->
        for x in 1..rows-1 do
            for y in 0..cols-1 do
                switch x y
    | West ->
        for y in 1..cols-1 do
            for x in 0..rows-1 do
                switch x y
    | South ->
        for x in rows-2..-1..0 do
            for y in 0..cols-1 do
                switch x y
    | East ->
        for y in cols-2..-1..0 do
            for x in 0..rows-1 do
                switch x y

let scoreOf grid =
    let rows = Array2D.length1 grid
    (0, grid) ||> Array2D.foldi (fun x _ score c ->
        if c = 'O'
        then score + rows - x
        else score)

let runPartOne content =
    let grid = parse content
    roll North grid
    scoreOf grid

let spinCycle grid =
    for dir in [ North; West; South; East ] do
        roll dir grid

let runPartTwo content =

    // https://en.wikipedia.org/wiki/Cycle_detection
    // https://www.educative.io/answers/why-does-floyds-cycle-detection-algorithm-work

    let seen = Dictionary()
    let cycleHead =
        (parse content, 0)
        |> Seq.unfold (fun (grid, index) ->
            let key = Array2D.toString grid
            if seen.ContainsKey key then
                None
            else
                seen.Add(key, index)
                spinCycle grid
                Some (grid, (grid, index+1)))
        |> Seq.last

    let cycleStart = seen[Array2D.toString cycleHead]
    let cycleLength = seen.Count - cycleStart
    let index = cycleStart + (1_000_000_000 - cycleStart) % cycleLength

    seen
    |> Seq.find (fun kv -> kv.Value = index)
    |> _.Key
    |> parse
    |> scoreOf
