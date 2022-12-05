open System
open System.Collections.Generic
open System.Text.RegularExpressions

let input = "    [D]
[N] [C]
[Z] [M] [P]
 1   2   3

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"

(* For performance we can use mutable data structures inside a function *)

type CrateMoverData =
    { Stacks: char array array
      Moves: (int * int * int) list }

type CrateMover = CrateMover9000 | CrateMover9001

let parse lines =
    let cratesPerLine = List()
    let moves = List()
    let regex = Regex @"move (\d+) from (\d+) to (\d+)"
    let mutable stacksCount = 0

    for line in lines do
        if String.IsNullOrWhiteSpace line then
            ()
        elif line.StartsWith " 1 " then
            stacksCount <- line.Split " " |> Array.last |> int
        elif line.StartsWith "move" then
            let g = regex.Match(line).Groups
            (int g[1].Value, int g[2].Value, int g[3].Value) |> moves.Add
        else
            Seq.chunkBySize 4 line
            |> Seq.map (fun cs -> cs[1])
            |> Seq.toArray
            |> cratesPerLine.Add

    let stacks = [| for _ in 1 .. stacksCount -> List() |]
    for crates in cratesPerLine do
        for i = 0 to crates.Length - 1 do
            if crates[i] <> ' ' then
                stacks[i].Add crates[i]

    { Stacks = stacks |> Array.map (fun l -> l.Reverse(); l.ToArray())
      Moves = Seq.toList moves }

let move crateMover data =
    let stacks = data.Stacks |> Array.map Stack
    for (count, from, to') in data.Moves do
        [| for _ in 1 .. count -> stacks[from-1].Pop() |]
        |> if crateMover = CrateMover9001 then Array.rev else id
        |> Array.iter stacks[to'-1].Push
    stacks |> Array.map (fun s -> s.Peek()) |> String

let data = input.Split "\n" |> parse
data |> move CrateMover9000 |> printfn "%s"
data |> move CrateMover9001 |> printfn "%s"
