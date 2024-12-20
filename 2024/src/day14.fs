module Day14

open System.Text.RegularExpressions

let Width = 11 // 101
let Height = 7 // 103
let HalfWidth = (Width - 1) / 2
let HalfHeight = (Height - 1) / 2

let parse input =
    let pattern = Regex "-?\d+"
    splitLines input |> Array.map (
        pattern.Matches >> Seq.map (_.Value >> int) >> Seq.toArray >>
        function arr -> arr[0], arr[1], arr[2], arr[3])

let printGrid robots =
    let grid = Array2D.create Height Width '.'
    for (x, y) in robots do
        grid[y, x] <- '#'
    Array2D.print grid

let teleport sec =
    Array.map (fun (px, py, vx, vy) ->
        (px + vx * sec) %% Width, (py + vy * sec) %% Height)

let runPartOne input =
    parse input
    |> teleport 100
    |> Array.fold (fun (tl, tr, bl, br) (x, y) ->
        match x, y with
        | x, y when x < HalfWidth && y < HalfHeight -> (tl + 1, tr, bl, br)
        | x, y when x > HalfWidth && y < HalfHeight -> (tl, tr + 1, bl, br)
        | x, y when x < HalfWidth && y > HalfHeight -> (tl, tr, bl + 1, br)
        | x, y when x > HalfWidth && y > HalfHeight -> (tl, tr, bl, br + 1)
        | _ -> (tl, tr, bl, br)) (0, 0, 0, 0)
    |> fun (tl, tr, bl, br) -> tl * tr * bl * br

let runPartTwo input =
    let start = parse input
    let getMax f = Array.groupBy f >> Array.maxBy (fun (_, g) -> g.Length) >> fun (_, g) -> g.Length

    let rec find sec =
        let robots = start |> teleport sec
        let maxX = robots |> getMax fst
        let maxY = robots |> getMax snd
        if maxX >= 30 && maxY >= 30 then
            printGrid robots
            sec
        else
            find (sec + 1)

    find 1
