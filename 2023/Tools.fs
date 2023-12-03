[<AutoOpen>]
module Tools

open System
open System.IO

let splitLines (str: string) =
    str.Split([| Environment.NewLine; "\n" |], StringSplitOptions.None)

let split (sep: char) (str: string) =
    str.Split(sep, StringSplitOptions.RemoveEmptyEntries)

let load (day: int32) =
    File.ReadAllLines $"""./inputs/day_{day.ToString "00"}.txt"""

let inline (>=<) num (left, right) = num >= left && num <= right
