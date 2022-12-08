open System
open System.Collections.Generic

let input = "vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"

(* Play with memoization and then with Set.intersectMany *)

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

let prioritizeMany = Set.intersectMany >> Seq.head >> prioritize

// Part One
input.Split '\n'
|> Seq.sumBy (findDuplicate >> prioritize)
|> printfn "%A"
// Or
input.Split '\n'
|> Seq.sumBy (Seq.splitInto 2 >> (Seq.map Set.ofSeq) >> prioritizeMany)
|> printfn "%A"

// Part Two
input.Split '\n'
|> Seq.map Set.ofSeq
|> Seq.chunkBySize 3
|> Seq.sumBy prioritizeMany
|> printfn "%A"
