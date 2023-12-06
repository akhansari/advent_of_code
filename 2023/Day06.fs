module Day06

open System

let betterTimings thresholdTime distRecord =
    let mutable count = 0L
    for holdTime in 0L..thresholdTime do
        if (thresholdTime-holdTime)*holdTime > distRecord then
            count <- count+1L
    count

let runPartOne (lines: string array) =
    let times = extractInt32 lines[0] |> Seq.toArray
    let distances = extractInt32 lines[1] |> Seq.toArray
    Seq.init times.Length id
    |> Seq.map (fun i -> betterTimings times[i] distances[i])
    |> Seq.fold (*) 1L

let runPartTwo (lines: string array) =
    let time = lines[0].Substring("Time:".Length).Replace(" ", "") |> Int64.Parse
    let distance = lines[1].Substring("Distance:".Length).Replace(" ", "") |> Int64.Parse
    betterTimings time distance
