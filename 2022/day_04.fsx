open System.Text.RegularExpressions

let input = "2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8"

let parse (data: string) =
    let regex = Regex(@"(\d+)-(\d+),(\d+)-(\d+)", RegexOptions.Compiled)
    data.Split '\n'
    |> Seq.map (fun l ->
        let g = regex.Match(l).Groups
        (int g[1].Value, int g[2].Value), (int g[3].Value, int g[4].Value))

// Part One
parse input
|> Seq.sumBy (fun ((f1, f2), (s1, s2)) ->
    let secondInFirst = s1 >= f1 && s2 <= f2
    let firstInSecond = f1 >= s1 && f2 <= s2
    if secondInFirst || firstInSecond then 1 else 0)
|> printfn "%A"

// Part Two
parse input
|> Seq.sumBy (fun ((f1, f2), (s1, s2)) ->
    if not (f2 < s1 || s2 < f1) then 1 else 0)
|> printfn "%A"
