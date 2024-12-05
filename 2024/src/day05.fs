module Day05

open System.Collections.Generic

let parse input =
    let parts = input |> split "\n\n" |> Array.map splitLines
    HashSet parts[0], parts[1] |> Array.map (split "," >> Array.map int >> Array.toList)

let sort (orders: HashSet<_>) =
    List.sortWith (fun a b -> if orders.Contains $"{a}|{b}" then -1 else 1)

let sum = Array.sumBy (fun (nums: int list) -> nums[nums.Length / 2])

let runPartOne input =
    let orders, lines = parse input

    lines |> Array.filter (fun nums -> nums = sort orders nums) |> sum

let runPartTwo input =
    let orders, lines = parse input

    lines
    |> Array.choose (fun nums ->
        let sorted = sort orders nums
        if nums = sorted then None else Some sorted)
    |> sum
