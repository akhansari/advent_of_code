module Day15

open System.Collections.Generic
open System.Collections.Specialized

let hashOf = Seq.fold (fun res c ->
    (res + int c) * 17 % 256) 0

let runPartOne content =
    content |> split "," |> Array.sumBy hashOf

let runPartTwo (content: string) =
    let boxes =
        seq {0..256}
        |> Seq.map (fun i -> KeyValuePair(i, OrderedDictionary()))
        |> Dictionary

    for step in content.Replace("-", "").Split(",") do
        match split "=" step with
        | [| lensLabel; focalLength |] ->
            boxes[hashOf lensLabel][lensLabel] <- int focalLength
        | [| lensLabel |] ->
            boxes[hashOf lensLabel].Remove lensLabel
        | _ ->
            failwith "bad input"

    boxes
    |> Seq.sumBy (fun box ->
        box.Value.Values
        |> Seq.cast<int>
        |> Seq.indexed
        |> Seq.sumBy (fun (i, focalLength) ->
            (box.Key+1) * (i+1) * focalLength))
