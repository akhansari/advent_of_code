module Day08

open System.Collections.Generic

let parse input =
    let arr = input |> splitLines |> Seq.map Array.ofSeq |> array2D

    let antennas =
        (Map.empty, arr)
        ||> Array2D.foldi (fun x y c antennas ->
            match c with
            | '.' -> antennas
            | _ ->
                antennas
                |> Map.change c (function
                    | Some lst -> (x, y) :: lst |> Some
                    | None -> Some [ (x, y) ]))

    (arr.GetLength 0, arr.GetLength 1, antennas)

let pairAll (items: _ list) =
    seq {
        for i in 0 .. items.Length - 1 do
            for j in i + 1 .. items.Length - 1 do
                items[i], items[j]
    }

let addAntinodes add =
    Map.iter (fun _ ->
        pairAll
        >> Seq.iter (fun (p1, p2) ->
            let diff = Point.subtract p1 p2
            add Point.add p1 diff
            add Point.subtract p2 diff))

let runPartOne input =
    let rows, cols, antennas = parse input
    let antinodes = HashSet()

    let ifInBound operation p diff =
        match operation p diff with
        | next when Point.inBound rows cols next -> antinodes.Add next |> ignore
        | _ -> ()

    antennas |> addAntinodes ifInBound
    antinodes.Count

let runPartTwo input =
    let rows, cols, antennas = parse input
    let antinodes = HashSet()

    let rec untilOutOfBound operation p diff =
        match operation p diff with
        | next when Point.inBound rows cols next ->
            antinodes.Add next |> ignore
            untilOutOfBound operation next diff
        | _ -> ()

    antennas.Values |> Seq.collect id |> Seq.iter (antinodes.Add >> ignore)
    antennas |> addAntinodes untilOutOfBound
    antinodes.Count
