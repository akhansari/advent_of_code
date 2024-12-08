module Day04

let parse = splitLines >> Array.map Array.ofSeq >> array2D

type Direction =
    | Left
    | Right
    | Up
    | Down
    | LeftUp
    | LeftDown
    | RightUp
    | RightDown

let nextPos (x, y) =
    function
    | Right -> (x, y + 1)
    | Left -> (x, y - 1)
    | Down -> (x + 1, y)
    | Up -> (x - 1, y)
    | LeftUp -> (x - 1, y - 1)
    | LeftDown -> (x + 1, y - 1)
    | RightUp -> (x - 1, y + 1)
    | RightDown -> (x + 1, y + 1)

let nextArrPos pos dir (arr: _[,]) =
    let next = nextPos pos dir
    if Array2D.inBound next arr then Some next else None

let countXmas startPos (arr: _[,]) =

    let rec find pos step dir =
        match arr.Get pos, step with
        | ('X', 1)
        | ('M', 2)
        | ('A', 3) ->
            match nextArrPos pos dir arr with
            | Some next -> find next (step + 1) dir
            | None -> 0
        | ('S', 4) -> 1
        | _ -> 0

    if 'X' <> arr.Get startPos then
        0
    else
        [| Left; Right; Up; Down; LeftUp; LeftDown; RightUp; RightDown |]
        |> Array.sumBy (find startPos 1)

let isXmas pos (arr: _[,]) =
    let get dir =
        nextArrPos pos dir arr |> Option.map arr.Get

    match arr.Get pos, get LeftUp, get RightDown, get LeftDown, get RightUp with
    | 'A', Some 'M', Some 'S', Some 'M', Some 'S'
    | 'A', Some 'S', Some 'M', Some 'M', Some 'S'
    | 'A', Some 'S', Some 'M', Some 'S', Some 'M'
    | 'A', Some 'M', Some 'S', Some 'S', Some 'M' -> true
    | _ -> false

let runPartOne input =
    let arr = parse input

    (0, arr) ||> Array2D.foldi (fun x y _ sum -> sum + countXmas (x, y) arr)

let runPartTwo input =
    let arr = parse input

    (0, arr)
    ||> Array2D.foldi (fun x y _ sum -> if isXmas (x, y) arr then sum + 1 else sum)
