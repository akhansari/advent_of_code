[<AutoOpen>]
module Tools

open System
open System.IO

let split (str: string) =
    str.Split([| "\n"; Environment.NewLine |], StringSplitOptions.None)

let load (day: int32) =
    File.ReadAllLines $"""./inputs/day_{day.ToString "00"}.txt"""
