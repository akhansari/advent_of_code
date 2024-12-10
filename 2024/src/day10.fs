module Day10

open System.Collections.Generic

type Direction = Left | Right | Up | Down

let nextPos (x, y) = function
    | Right -> (x, y + 1)
    | Left  -> (x, y - 1)
    | Down  -> (x + 1, y)
    | Up    -> (x - 1, y)

let parse =
    splitLines >> Seq.map (Array.ofSeq >> Array.map (string >> int)) >> array2D

let rate isUnique plan =

    let countPaths start =
        let visited = HashSet()
        let rec dfs (x, y) num =
            if not <| Array2D.inBound (x, y) plan then 0
            elif visited.Contains (x, y) then 0
            elif plan[x, y] <> num then 0
            elif plan[x, y] = 9 then
                if isUnique then
                    visited.Add (x, y) |> ignore
                1
            else
                [ Right; Left; Down; Up ]
                |> List.sumBy (fun dir -> dfs (nextPos (x, y) dir) (num + 1))
        dfs start 0

    (List.empty, plan)
    ||> Array2D.foldi (fun x y num zeros ->
        if num = 0 then (x, y) :: zeros else zeros)
    |> List.sumBy countPaths


let runPartOne input =
    parse input |> rate true

let runPartTwo input =
    parse input |> rate false
