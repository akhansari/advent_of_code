module Day07

open System

type Hand =
    | HighCard
    | OnePair
    | TwoPair
    | ThreeKind
    | FullHouse
    | FourKind
    | FiveKind

let hand setJokers cards =
    cards
    |> Seq.toList
    |> List.countBy id
    |> List.map snd
    |> List.sortDescending
    |> setJokers
    |> function
    | [ 5 ]       -> FiveKind
    | [ 4; 1 ]    -> FourKind
    | [ 3; 2 ]    -> FullHouse
    | 3 :: _      -> ThreeKind
    | [ 2; 2; 1 ] -> TwoPair
    | 2 :: _      -> OnePair
    | _           -> HighCard

module PartOne =
    let mapCard = function
        | 'T' -> 10 | 'J' -> 11 | 'Q' -> 12 | 'K' -> 13 | 'A' -> 14
        | c -> Char.GetNumericValue c |> int
    let hand = hand id
    let mapSort cards =
        hand cards, cards |> Seq.map (mapCard >> sprintf "%02i") |> String.concat ""

module PartTwo =
    let mapCard = function
        | 'T' -> 10 | 'J' -> 1 | 'Q' -> 12 | 'K' -> 13 | 'A' -> 14
        | c -> Char.GetNumericValue c |> int
    let hand (cards: string) =
        let jokers = cards |> Seq.filter ((=) 'J') |> Seq.length
        let setJokers = function first :: tail -> first+jokers :: tail | [] -> [ 5 ]
        cards.Replace("J", "") |> hand setJokers
    let mapSort cards =
        hand cards, cards |> Seq.map (mapCard >> sprintf "%02i") |> String.concat ""

let run mapSort lines =
    lines
    |> Array.map (fun line ->
        let parts = split " " line
        parts[0], int parts[1])
    |> Array.sortBy (fst >> mapSort)
    |> Array.fold (fun (sum, rank) (_, bids) -> sum+(bids*rank), rank+1) (0, 1)
    |> fst

let runPartOne lines = run PartOne.mapSort lines
let runPartTwo lines = run PartTwo.mapSort lines
