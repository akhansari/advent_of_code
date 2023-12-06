module Day06

open System

let betterTimings thresholdTime distRecord =
    Seq.init thresholdTime id
    |> Seq.choose (fun holdTime ->
        match (thresholdTime-holdTime)*holdTime with
        | dist when dist > distRecord -> Some (holdTime, dist)
        | _ -> None)

let runPartOne (lines: string array) =
    let times = extractInt32 lines[0] |> Seq.toArray
    let distances = extractInt32 lines[1] |> Seq.toArray
    Seq.init times.Length id
    |> Seq.map (fun i -> betterTimings times[i] distances[i] |> Seq.length)
    |> Seq.fold (*) 1

let runPartTwo (lines: string array) =
    let time = lines[0].Substring("Time:".Length).Replace(" ", "") |> Double.Parse
    let distance = lines[1].Substring("Distance:".Length).Replace(" ", "") |> Double.Parse
    sqrt(time**2 - 4.*distance) |> int64
