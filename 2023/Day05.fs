module Day05

open System

let parse str =
    let chunks = split "\n\n" str
    let seeds = extractInt64 chunks[0] |> Seq.toArray
    let allMappings = [|
        for chunk in chunks |> Array.skip 1 do [|
            for line in chunk |> split "\n" |> Array.skip 1 do
                extractInt64 line |> Seq.toArray
        |]
    |]
    seeds, allMappings

let locationOf (allMappings: Int64[][][]) seed =
    (seed, allMappings)
    ||> Array.fold (fun cur mappings ->
        mappings
        |> Array.tryPick (fun m ->
            if cur >=< (m[1], m[1]+m[2])
            then m[0] - m[1] + cur |> Some
            else None)
        |> Option.defaultValue cur)

let runPartOne str =
    let seeds, allMappings = parse str
    seeds
    |> Array.fold (fun minimum seed ->
        locationOf allMappings seed |> min minimum
        ) Int64.MaxValue

// brute force
let runPartTwo str =
    let seeds, allMappings = parse str
    seeds
    |> Array.splitInto (seeds.Length / 2)
    |> Array.collect (fun range ->
        let arr = ResizeArray()
        for i in range[0]..range[0]+range[1]-1L do
            arr.Add i
        arr.ToArray())
    |> Array.fold (fun minimum seed ->
        locationOf allMappings seed |> min minimum
        ) Int64.MaxValue
