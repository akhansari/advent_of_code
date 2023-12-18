module Day04

type Card = { Id: int; Winning: Set<int>; Player: Set<int> }

let parse line =
    let get = extractInt32 >> Set
    let card = split ":" line
    let numbers = split "|" card[1]
    { Id = extractInt32 card[0] |> Seq.head
      Winning = get numbers[0]
      Player = get numbers[1] }

let findMatchingNumbers card =
    Set.intersect card.Winning card.Player

let runPartOne lines =
    lines
    |> Array.sumBy (fun lines ->
        let count = lines |> parse |> findMatchingNumbers |> Set.count
        2. ** (float count - 1.) |> int)

let runPartTwo lines =

    let cardsMatchingNumbers = lines |> Array.map (fun line ->
        let card = parse line
        card.Id, findMatchingNumbers card |> Set.count)

    (Seq.map (fun (id, _) -> id, 1) cardsMatchingNumbers |> Map, cardsMatchingNumbers)
    ||> Array.fold (fun copies (cardId, matchingNumbers) ->
        let rec update id innerCopies =
            if id > (cardId + matchingNumbers) then
                innerCopies
            else
                innerCopies
                |> Map.change id (function
                    | Some n -> n + innerCopies[cardId] |> Some
                    | v -> v)
                |> update (id + 1)
        update (cardId + 1) copies)
    |> Map.fold (fun sum _ copies -> sum + copies) 0
