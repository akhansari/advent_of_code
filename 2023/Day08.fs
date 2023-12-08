module Day08

open Microsoft.FSharp.Core.Operators.Checked

let parse (lines: string array) =
    let directions =
        lines[0]
        |> Seq.map (function 'L' -> 0 | 'R' -> 1 | _ -> failwith "bad direction")
        |> Seq.toArray
    let mappings = lines[2..] |> Seq.map (fun line ->
        let parts = split " = (" line[..line.Length-2]
        let tuple = split ", " parts[1]
        parts[0], tuple)
    directions, Map mappings

let runPartOne lines =
    let directions, mappings = parse lines
    let rec find steps key dir =
        if key = "ZZZ" then steps-1 else
            let nextKey = mappings[key][dir]
            let nextDir = directions[steps % directions.Length]
            find (steps+1) nextKey nextDir
    find 1 (Seq.head mappings.Keys) directions[0]

let rec gcd (a: int64) (b: int64) = if b = 0 then a else gcd b (a % b)
let lcm a b = (a * b) / gcd a b

let runPartTwo lines =
    let directions, mappings = parse lines
    let rec find (steps: int64) (key: string) dir =
        if key[2] = 'Z' then steps-1L else
            let nextKey = mappings[key][dir]
            let nextDir = directions[steps % (int64 directions.Length) |> int]
            find (steps+1L) nextKey nextDir
    mappings.Keys |> Seq.fold (fun ans key ->
        if key[2] = 'A'
        then find 1 key directions[0] |> lcm ans
        else ans
        ) 1L
