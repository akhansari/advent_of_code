[<AutoOpen>]
module Tools

open System
open System.IO

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

let printv o = printfn $"%A{o}"
let print o = printv o; o

let printAll o =
    Seq.iter (printfn "%A") o
    o

module Array2D =

    let find predicate (arr: _[,]) =
        let rec go x y =
            if y >= arr.GetLength 1 then None
            elif x >= arr.GetLength 0 then go 0 (y + 1)
            elif predicate arr[x, y] then Some(x, y)
            else go (x + 1) y
        go 0 0

    let foldi (folder: int -> int -> 'T -> 'S -> 'S) (state: 'S) (array: 'T[,]) =
        let mutable state = state
        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                state <- folder x y array[x, y] state
        state

    let inBound (x, y) (arr: _[,]) =
        let rows = Array2D.length1 arr
        let cols = Array2D.length2 arr
        x >= 0 && y >= 0 && x < rows && y < cols

    let copy (arr: _[,]) =
        Array2D.init (Array2D.length1 arr) (Array2D.length2 arr) (fun x y -> arr[x, y])

    let fromString = splitLines >> Seq.map Array.ofSeq >> array2D

    let toString (arr: _[,]) =
        let sb = Text.StringBuilder()
        for x in 0 .. Array2D.length1 arr - 1 do
            sb.Append(arr[x, *]).Append("\n") |> ignore
        sb.Remove(sb.Length - 1, 1).ToString()

module Point =

    let subtract (x1, y1) (x2, y2) = (x1 - x2, y1 - y2)

    let add (x1, y1) (x2, y2) = (x1 + x2, y1 + y2)

    let inBound rows cols (x, y) =
        x >= 0 && x < rows && y >= 0 && y < cols

type ``[,]``<'T> with
    member this.Get(x, y) = this[x, y]

