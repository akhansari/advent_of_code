open System

let input = "A Y
B X
C Z"

let (|Rock|Paper|Scissors|) = function
    | 'A' | 'X' -> Rock
    | 'B' | 'Y' -> Paper
    | 'C' | 'Z' -> Scissors
    | c -> ArgumentOutOfRangeException($"Shape '{c}'") |> raise

let (|Win|Draw|Loss|) = function
    | Rock, Scissors | Paper, Rock | Scissors, Paper -> Win
    | Rock, Rock | Paper, Paper | Scissors, Scissors -> Draw
    | Rock, Paper | Paper, Scissors | Scissors, Rock -> Loss

let (|Weight|) = function
    Rock -> 1 | Paper -> 2 | Scissors -> 3

let (|ToWin|ToDraw|ToLose|) = function
    | 'X' -> ToLose | 'Y' -> ToDraw | 'Z' -> ToWin
    | c -> ArgumentOutOfRangeException($"Shape '{c}'") |> raise

let (|ReverseWeight|) = function
    | ToDraw, Rock | ToWin, Scissors | ToLose, Paper -> 1
    | ToDraw, Paper | ToWin, Rock | ToLose, Scissors -> 2
    | ToDraw, Scissors | ToWin, Paper | ToLose, Rock -> 3

let strategyOne turns =
    turns |> Seq.sumBy (fun (opponent, player) ->
        match (player, opponent), player with
        | Win,  Weight w -> w + 6
        | Draw, Weight w -> w + 3
        | Loss, Weight w -> w + 0)

let strategyTwo turns =
    turns |> Seq.sumBy (fun (opponent, todo) ->
        match todo, (todo, opponent) with
        | ToWin,  ReverseWeight w -> w + 6
        | ToDraw, ReverseWeight w -> w + 3
        | ToLose, ReverseWeight w -> w)

let parse (data: string) =
    data.Split '\n' |> Seq.map (fun line -> line[0], line[2])

input |> parse |> strategyOne |> printfn "%i"
input |> parse |> strategyTwo |> printfn "%i"