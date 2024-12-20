module Day15

open System.Collections.Generic

let findRobot = Array2D.find ((=) '@') >> Option.get

let parse input =
    let parts = split "\n\n" input
    let grid = parts[0] |> Array2D.fromString
    let moves =
        parts[1]
        |> Seq.choose (function
            | 'v' -> Some Point.Down
            | '^' -> Some Point.Up
            | '<' -> Some Point.Left
            | '>' -> Some Point.Right
            | _ -> None)
        |> Seq.toList
    let robot = findRobot grid
    grid, moves, robot

let swap (grid: _[,]) (x1, y1) (x2, y2) =
    let tmp = grid[x1, y1]
    grid[x1, y1] <- grid[x2, y2]
    grid[x2, y2] <- tmp

let findTargets (grid: _[,]) dir robot =
    let targets = HashSet()
    let mutable blocked = false
    let rec find pos =
        match grid.Get pos with
        | '#' -> blocked <- true
        | '@' | 'O' ->
            if targets.Add pos then
                find (Point.nextPos pos dir)
        | '[' | ']' ->
            if targets.Add pos then
                find (Point.nextPos pos dir)
            let other =
                if grid.Get pos = '[' then Point.Right else Point.Left
                |> Point.nextPos pos
            if targets.Add other then
                find (Point.nextPos other dir)
        | _ -> ()
    find robot
    if blocked then List.empty else List.ofSeq targets

let moveTargets (grid: _[,]) dir targets =
    match dir with
    | Point.Left -> targets |> List.sort
    | Point.Right -> targets |> List.sortDescending
    | Point.Up -> targets |> List.sortBy (fun (x, y) -> y, x)
    | Point.Down -> targets |> List.sortByDescending (fun (x, y) -> y, x)
    |> List.iter (fun pos ->
        Point.nextPos pos dir |> swap grid pos)

let rec move (grid: _[,]) moves robot =
    match moves with
    | [] -> ()
    | dir :: rest ->
        match findTargets grid dir robot with
        | [] -> robot
        | targets ->
            moveTargets grid dir targets
            findRobot grid
        |> move grid rest

let runPartOne input =
    let grid, moves, robot = parse input

    move grid moves robot

    grid |> Array2D.foldi (fun x y c sum ->
        if c = 'O' then sum + (100 * x + y) else sum) 0

let runPartTwo (input: string) =
    let grid, moves, robot =
        input.Replace("#", "##").Replace("O", "[]").Replace(".", "..").Replace("@", "@.")
        |> parse

    move grid moves robot

    grid |> Array2D.foldi (fun x y c sum ->
        if c = '[' then sum + (100 * x + y) else sum) 0

