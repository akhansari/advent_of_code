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
    Regex(@"-?\d+").Matches str
    |> Seq.map (fun m -> int32 m.Value)

let extractInt64 str =
    Regex(@"-?\d+").Matches str
    |> Seq.map (fun m -> int64 m.Value)

let measureElapsedTime () =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    { new IDisposable with
        member _.Dispose() = printfn $"{sw.Elapsed}" }

let print o = printfn $"%A{o}"; o
let printAll o = Seq.iter (printfn "%A") o; o

module Array2D =

    let find value (arr: _ [,]) =
        let rec go x y =
            if   y >= arr.GetLength 1 then None
            elif x >= arr.GetLength 0 then go 0 (y+1)
            elif arr[x,y] = value     then Some (x,y)
            else go (x+1) y
        go 0 0

    let foldi (folder: int -> int -> 'S -> 'T -> 'S) (state: 'S) (array: 'T[,]) =
        let mutable state = state
        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                state <- folder x y state array[x, y]
        state
