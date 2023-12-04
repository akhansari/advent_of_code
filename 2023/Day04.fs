module Day04

open System
open System.Text.RegularExpressions

type Card = { Id: int; Winning: Set<int>; Player: Set<int> }

let parse line =
    let extractNumbers str =
        Regex("[0-9]+").Matches str
        |> Seq.map (fun m -> Int32.Parse m.Value)
        |> Set
    let card = split ':' line
    let numbers = split '|' card[1]
    { Id = extractNumbers card[0] |> Seq.head
      Winning = extractNumbers numbers[0]
      Player = extractNumbers numbers[1] }

let findMatchingNumbers card =
    Set.intersect card.Winning card.Player

let runPartOne =
    parse
    >> findMatchingNumbers
    >> fun numbers -> 2. ** (float numbers.Count - 1.) |> int
    |> Array.sumBy

let runPartTwo lines =

    let cards =
        lines
        |> Array.map (fun line ->
            let card = parse line
            card.Id, findMatchingNumbers card |> Set.count)

    let initialCopies =
        cards |> Seq.map (fun (id, _) -> id, 1) |> Map

    (initialCopies, cards)
    ||> Array.fold
        (fun copies (cardId, matchingNumbers) ->
            let rec update id innerCopies =
                if id > (cardId + matchingNumbers) then
                    innerCopies
                else
                    innerCopies
                    |> Map.change id (function
                        | Some v -> v + innerCopies[cardId] |> Some
                        | None -> Some innerCopies[cardId])
                    |> update (id + 1)
            update (cardId + 1) copies)
    |> Map.fold (fun sum _ copies -> sum + copies) 0
