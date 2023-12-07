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

let hand mapCard fixCounts cards =
    cards
    |> Seq.toList
    |> List.countBy id
    |> List.sortByDescending (fun (card, length) -> length, mapCard card)
    |> List.map snd
    |> fixCounts
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
    let hand =
        hand mapCard id
    let mapSort cards =
        hand cards, cards |> Seq.map (mapCard >> sprintf "%02i") |> String.concat ""

module PartTwo =
    let mapCard = function
        | 'T' -> 10 | 'J' -> 1 | 'Q' -> 12 | 'K' -> 13 | 'A' -> 14
        | c -> Char.GetNumericValue c |> int
    let hand (cards: string) =
        let jokers = cards |> Seq.filter ((=) 'J') |> Seq.length
        let fix = function first :: tail -> first+jokers :: tail | [] -> [ 5 ]
        cards.Replace("J", "") |> hand mapCard fix
    let mapSort (cards: string) =
        hand cards, cards |> Seq.map (mapCard >> sprintf "%02i") |> String.concat ""

let run mapSort lines =
    lines
    |> Array.map (fun line ->
        let parts = split " " line
        parts[0], int parts[1])
    |> Array.sortBy (fst >> mapSort)
    |> Array.fold (fun (sum, rank) (_, bids) -> sum+(bids*rank), rank+1) (0, 1)
    |> fst

let runPartOne = run PartOne.mapSort
let runPartTwo = run PartTwo.mapSort
