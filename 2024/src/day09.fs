module Day09

open System.Collections.Generic
open Microsoft.FSharp.Core.Operators.Checked

[<Struct>]
type Block =
    | File of int * int
    | FreeSpace of int

let printDiskMap (diskMap: Block seq) =
    diskMap
    |> Seq.iter (function
        | File (i, len) -> String.replicate len (string i) |> printf "%s"
        | FreeSpace len -> String.replicate len "." |> printf "%s")
    printfn ""

let runPartOne (input: string) =

    let diskMap, count =
        ((0, 0), input)
        ||> Seq.mapFold (fun (i, count) c ->
            let len = c |> string |> int
            if (i + 1) % 2 = 0
            then FreeSpace len, (i + 1, count)
            else File (i / 2, len), (i + 1, count + len))
        |> fun (diskMap, (_, count)) -> Seq.toList diskMap, count

    let readFromStart =
        seq {
            for item in diskMap do
                match item with
                | File (id, len) -> for _ in 1 .. len do id
                | FreeSpace len -> for _ in 1 .. len do -1
        }

    let readFromEnd =
        seq {
            for i = diskMap.Length - 1 downto 0 do
                match diskMap[i] with
                | File (id, len) -> for _ in 1 .. len do id
                | FreeSpace _ -> ()
        }

    use fromStart = readFromStart.GetEnumerator()
    use fromEnd = readFromEnd.GetEnumerator()

    seq { 0 .. count - 1 }
    |> Seq.sumBy (fun i ->
        fromStart.MoveNext() |> ignore
        if fromStart.Current = -1 then
            fromEnd.MoveNext() |> ignore
            fromEnd.Current * i |> int64
        else
            fromStart.Current * i |> int64)

let runPartTwo (input: string) =
    let ll = LinkedList()
    let files = Stack()

    input
    |> Seq.iteri (fun i c ->
        let len = c |> string |> int
        if (i + 1) % 2 = 0
        then FreeSpace len |> ll.AddLast |> ignore
        else File (i / 2, len) |> ll.AddLast |> files.Push)

    while files.Count > 0 do
        let file = files.Pop()
        let mutable node = ll.First
        while node <> null && node <> file do
            match node.Value, file.Value with
            | FreeSpace freeSpaceLen, File (id, fileLen) when freeSpaceLen >= fileLen ->
                let fileBefore = node.Previous
                ll.Remove node
                ll.AddAfter(file.Previous, FreeSpace fileLen) |> ignore
                ll.Remove file
                let newBlock = ll.AddAfter(fileBefore, File (id, fileLen))
                let diff = freeSpaceLen - fileLen
                if diff > 0 then
                    ll.AddAfter(newBlock, FreeSpace diff) |> ignore
            | _ -> ()
            node <- node.Next

    seq {
        for node in ll do
            match node with
            | File (id, len) -> for _ in 1 .. len do id
            | FreeSpace len -> for _ in 1 .. len do -1
    }
    |> Seq.indexed
    |> Seq.sumBy (fun (i, id) -> if id = -1 then 0L else id * i |> int64)

