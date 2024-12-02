[<AutoOpen>]
module Tools

open System
open System.IO
open System.Text.RegularExpressions

let inline (>=<) num (left, right) = num >= left && num <= right

let private getPath day =
    if Path.Exists $"./inputs/day%02i{day}.txt" then
        $"./inputs/day%02i{day}.txt"
    else
        $"../../../inputs/day%02i{day}.txt"

let loadText day = getPath day |> File.ReadAllText

let splitLines (str: string) =
    str.Split([| Environment.NewLine; "\n" |], StringSplitOptions.None)

let split (sep: string) (str: string) =
    str.Split(sep, StringSplitOptions.RemoveEmptyEntries)

let measureElapsedTime () =
    let sw = Diagnostics.Stopwatch.StartNew()

    { new IDisposable with
        member _.Dispose() = printfn $"elapsed time: {sw.Elapsed}" }

let print o =
    printfn $"%A{o}"
    o

let printAll o =
    Seq.iter (printfn "%A") o
    o

module Array2D =

    let find value (arr: _[,]) =
        let rec go x y =
            if y >= arr.GetLength 1 then None
            elif x >= arr.GetLength 0 then go 0 (y + 1)
            elif arr[x, y] = value then Some(x, y)
            else go (x + 1) y

        go 0 0

    let foldi (folder: int -> int -> 'S -> 'T -> 'S) (state: 'S) (array: 'T[,]) =
        let mutable state = state

        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                state <- folder x y state array[x, y]

        state

    let inBound (x, y) (arr: _[,]) =
        let rows = Array2D.length1 arr
        let cols = Array2D.length2 arr
        x >= 0 && y >= 0 && x < rows && y < cols

    let copy (arr: _[,]) =
        Array2D.init (Array2D.length1 arr) (Array2D.length2 arr) (fun x y -> arr[x, y])

    let toString (arr: _[,]) =
        let sb = Text.StringBuilder()

        for x in 0 .. Array2D.length1 arr - 1 do
            sb.Append(arr[x, *]).Append("\n") |> ignore

        sb.Remove(sb.Length - 1, 1).ToString()
