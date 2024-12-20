module Day15

open System.Collections.Generic

let parse transform input =
    let parts = split "\n\n" input
    let grid = parts[0] |> transform |> Array2D.fromString
    let moves =
        parts[1]
        |> Seq.choose (function
            | 'v' -> Some Point.Down
            | '^' -> Some Point.Up
            | '<' -> Some Point.Left
            | '>' -> Some Point.Right
            | _ -> None)
        |> Seq.toList
    let robot = grid |> Array2D.find ((=) '@') |> Option.get
    grid, moves, robot

let runPartOne input =

    let findTargets (grid: _[,]) dir robot =
        let rec find pos targets =
            match grid.Get pos with
            | '#' -> None // cannot be moved
            | 'O' | '@' ->
                let next = Point.nextPos pos dir
                (pos, next) :: targets |> find next
            | _ -> targets |> List.rev |> Some
        find robot []

    let moveTargets (grid: _[,]) targets =
        let (sx, sy), (rx, ry) = List.head targets
        grid[sx, sy] <- '.'
        grid[rx, ry] <- '@'
        if targets.Length > 1 then
            let _, (nx, ny) = List.last targets
            grid[nx, ny] <- 'O'
        (rx, ry)

    let rec move (grid: _[,]) moves robot =
        match moves with
        | [] -> ()
        | dir :: rest ->
            let next =
                findTargets grid dir robot
                |> Option.map (moveTargets grid)
                |> Option.defaultValue robot
            move grid rest next

    let grid, moves, robot = parse id input
    move grid moves robot

    grid |> Array2D.foldi (fun x y c sum ->
        if c = 'O' then sum + (100 * x + y) else sum) 0

let runPartTwo input =

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
            | '@' ->
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
            let next =
                let targets = findTargets grid dir robot
                if targets.IsEmpty then
                    robot
                else
                    moveTargets grid dir targets
                    grid |> Array2D.find ((=) '@') |> Option.get
            move grid rest next

    let transform (str: string) =
        str.Replace("#", "##").Replace("O", "[]").Replace(".", "..").Replace("@", "@.")
    let grid, moves, robot = parse transform input
    move grid moves robot

    grid |> Array2D.foldi (fun x y c sum ->
        if c = '[' then sum + (100 * x + y) else sum) 0
