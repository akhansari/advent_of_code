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
|> Array.map (fun elf -> elf.Split '\n' |> Array.sumBy int)
|> Array.max
|> stdout.WriteLine
