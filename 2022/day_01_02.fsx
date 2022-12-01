let data = """1000
2000
3000

4000

5000
6000

7000
8000
9000

10000"""

data.Split "\n\n"
|> Seq.map (fun elf -> elf.Split '\n' |> Seq.sumBy int)
|> Seq.sortDescending
|> Seq.take 3
|> Seq.sum
|> stdout.WriteLine
