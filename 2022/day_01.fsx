let input = "1000
2000
3000

4000

5000
6000

7000
8000
9000

10000"

let parse (data: string) =
    data.Split "\n\n"
    |> Seq.map (fun elf -> elf.Split '\n' |> Seq.sumBy int)

let max  = parse >> Seq.max

let top3 = parse >> Seq.sortDescending >> Seq.take 3 >> Seq.sum

max  input |> printfn "%i" // Part One
top3 input |> printfn "%i" // Part Two
