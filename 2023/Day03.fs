module Day03

open System
open System.Collections.Generic
open System.Text.RegularExpressions

let extractNumbers str =
    Regex(@"\d+").Matches str
    |> Seq.map (fun m ->
        {| Value = Int32.Parse m.Value
           StartIndex = m.Index
           EndIndex = m.Index + m.Value.Length |})
    |> Seq.toList

let runPartOne (lines: string array) =
    lines
    |> Seq.mapi (fun row line ->
        extractNumbers line
        |> List.sumBy (fun num ->
            let chars = HashSet()
            let add c = chars.Add c |> ignore
            if num.StartIndex > 0 then add line[num.StartIndex-1] // before
            if num.EndIndex < line.Length then add line[num.EndIndex] // after
            if row > 0 then lines[row-1][num.StartIndex-1..num.EndIndex] |> Seq.iter add // above
            if row < lines.Length-1 then lines[row+1][num.StartIndex-1..num.EndIndex] |> Seq.iter add // below
            if Seq.tryExactlyOne chars = Some '.' then 0 else num.Value))
    |> Seq.sum

let runPartTwo (lines: string array) =

    let data = lines |> Array.mapi (fun index line ->
        {| Row = index
           Numbers = extractNumbers line
           Gears = Regex(@"\*").Matches line |> Seq.map (fun m -> m.Index) |> Seq.toArray |})

    data
    |> Array.sumBy (fun line -> line.Gears |> Array.sumBy (fun gear ->
        let adjacent = [
             // on the side
             yield! line.Numbers |> List.choose (fun num ->
                 if gear = num.StartIndex-1 || gear = num.EndIndex then Some num else None)
             // above
             if line.Row > 0 then
                 yield! data[line.Row-1].Numbers
                 |> List.choose (fun num ->
                     if gear >=< (num.StartIndex-1, num.EndIndex) then Some num else None)
             // below
             if line.Row < data.Length-1 then
                 yield! data[line.Row+1].Numbers
                 |> List.choose (fun num ->
                     if gear >=< (num.StartIndex-1, num.EndIndex) then Some num else None)
        ]
        if adjacent.Length = 2 then adjacent[0].Value * adjacent[1].Value else 0))
