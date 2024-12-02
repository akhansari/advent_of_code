module Day01

open System

let parse =
    splitLines >> Array.map (split " " >> Array.map Int32.Parse) >> Array.transpose

let runPartOne input =
    let leftRight = parse input

    Array.map2 (fun a b -> a - b |> abs) (Array.sort leftRight[0]) (Array.sort leftRight[1])
    |> Array.sum

let runPartTwo input =
    let leftRight = parse input

    let counts =
        leftRight[1]
        |> Array.fold
            (fun map num ->
                map
                |> Map.change num (function
                    | Some value -> Some(value + num)
                    | None -> Some num))
            Map.empty

    leftRight[0]
    |> Array.sumBy (fun num -> Map.tryFind num counts |> Option.defaultValue 0)
