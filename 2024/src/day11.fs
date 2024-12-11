module Day11

open System.Collections.Generic

let parse = split " " >> Array.map int64

let countDigits num =
    num |> double |> log10 |> floor |> ((+) 1.) |> int

let (|EvenSplit|_|) (num: int64) =
    match countDigits num with
    | cnt when cnt % 2 = 0 ->
        let half = cnt / 2
        let a = double num / (10. ** half) |> int64
        let b = double num % (10. ** half) |> int64
        Some (a, b)
    | _ -> None

type Evolved = One of int64 | Two of int64 * int64

let evolveStone = function
    | 0L -> One 1L
    | EvenSplit v -> Two v
    | num -> num * 2024L |> One

let evolve times start =
    let cache = Dictionary()

    let rec loop step stone =
        if step = 0 then 1L else
        match cache with
        | ValueOfKey (stone, step) len -> len
        | _ ->
            let len =
                match evolveStone stone with
                | One stone ->
                    loop (step - 1) stone
                | Two (stone1, stone2) ->
                    loop (step - 1) stone1 + loop (step - 1) stone2
            cache.Add((stone, step), len)
            len

    start |> Array.map (loop times) |> Array.sum

let runPartOne input =
    parse input |> evolve 25

let runPartTwo input =
    parse input |> evolve 75

(*
Depth-first traversal and caching is again the key here.
At each step (blink), we cache the pair of stone and length.
So when we encounter the same stone at the same step again, we can just return the cached length.
*)
