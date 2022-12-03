open System
open System.Collections.Generic

let input = "vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"

(* Play with memoization and then with Set.intersect *)

let findDuplicate (rucksack: string) =
    let hs = HashSet()
    let half = rucksack.Length / 2
    let rec find i =
        if i < half then
            hs.Add rucksack[i] |> ignore
            find (i + 1)
        elif hs.Contains rucksack[i] then
            rucksack[i]
        else
            find (i + 1)
    find 0

let prioritize c =
    (int c) - (if Char.IsLower c then (int 'a') else (int 'A' - 26)) + 1

// Part One
input.Split '\n'
|> Seq.map (findDuplicate >> prioritize)
|> Seq.sum
|> printfn "%A"

// Part Two
input.Split '\n'
|> Seq.map Set.ofSeq
|> Seq.chunkBySize 3
|> Seq.sumBy (Set.intersectMany >> Seq.head >> prioritize)
|> printfn "%A"
