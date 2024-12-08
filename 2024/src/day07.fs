module Day07

let parse =
    splitLines
    >> Array.map (fun line ->
        let parts = split ": " line
        int64 parts[0], split " " parts[1] |> Array.map int64)

let hasExpressions operators target nums =
    let rec loop i num =
        if i = Array.length nums then
            num = target
        else
            operators |> Array.exists (fun op -> op num nums[i] |> loop (i + 1))

    loop 1 nums[0]

let totalCalibration operators input =
    parse input
    |> Array.sumBy (fun (target, nums) -> if hasExpressions operators target nums then target else 0)

let runPartOne input = totalCalibration [| (+); (*) |] input

let runPartTwo input =
    totalCalibration [| (+); (*); (fun a b -> int64 $"{a}{b}") |] input
