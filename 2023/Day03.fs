module Day03

open System
open System.Collections.Generic
open System.Text.RegularExpressions

let extractNumbers str =
    Regex("[0-9]+").Matches str
    |> Seq.map (fun m -> Int32.Parse m.Value, m.Index, m.Value.Length)

let runPartOne (lines: string array) =
    lines
    |> Seq.mapi (fun row line ->
        extractNumbers line
        |> Seq.sumBy (fun (num, numIndex, numLen) ->
            let chars = HashSet()
            let add c = chars.Add c |> ignore
            if numIndex > 0 then add line[numIndex-1] // before
            if numIndex+numLen < line.Length then add line[numIndex+numLen] // after
            if row > 0 then lines[row-1][numIndex-1..numIndex+numLen] |> Seq.iter add // above
            if row < lines.Length-1 then lines[row+1][numIndex-1..numIndex+numLen] |> Seq.iter add // below
            if Seq.tryExactlyOne chars = Some '.' then 0 else num))
    |> Seq.sum

let runPartTwo (lines: string array) =
    let data =
        lines
        |> Seq.mapi (fun index line ->
            {| Row = index
               Numbers = extractNumbers line
               Gears = Regex(@"\*").Matches line |> Seq.map (fun m -> m.Index) |> Seq.toList |})
        |> Seq.toList
    data
    |> Seq.filter (fun line -> line.Gears.Length > 0)
    |> Seq.sumBy (fun line ->
        line.Gears
        |> Seq.sumBy (fun gear ->
            let numbers = [
                 // behind
                 yield! line.Numbers |> Seq.choose (fun (num, numIndex, numLen) ->
                     if gear = numIndex+numLen || gear = numIndex-1 then Some num else None)
                 // above
                 if line.Row > 0 then
                     yield! data[line.Row-1].Numbers
                     |> Seq.choose (fun (num, numIndex, numLen) ->
                         if gear >=< (numIndex-1, numIndex+numLen) then Some num else None)
                 // below
                 if line.Row < data.Length-1 then
                     yield! data[line.Row+1].Numbers
                     |> Seq.choose (fun (num, numIndex, numLen) ->
                         if gear >=< (numIndex-1, numIndex+numLen) then Some num else None)
            ]
            if numbers.Length = 2 then numbers[0] * numbers[1] else 0))
