[<AutoOpen>]
module Tools

open System
open System.IO
open System.Text.RegularExpressions

let splitLines (str: string) =
    str.Split([| Environment.NewLine; "\n" |], StringSplitOptions.None)

let split (sep: string) (str: string) =
    str.Split(sep, StringSplitOptions.RemoveEmptyEntries)

let loadLines (day: int32) =
    File.ReadAllLines $"""./inputs/day_{day.ToString "00"}.txt"""

let loadText (day: int32) =
    File.ReadAllText $"""./inputs/day_{day.ToString "00"}.txt"""

let inline (>=<) num (left, right) = num >= left && num <= right

let extractInt32 str =
    Regex(@"\d+").Matches str
    |> Seq.map (fun m -> int32 m.Value)

let extractInt64 str =
    Regex(@"\d+").Matches str
    |> Seq.map (fun m -> int64 m.Value)

let measureElapsedTime () =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    { new IDisposable with
        member _.Dispose() = printfn $"{sw.Elapsed}" }
