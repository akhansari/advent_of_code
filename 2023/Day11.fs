module Day11

let parse (lines: string array) =
   lines |> Array.map Array.ofSeq |> array2D

let rec getCombinations = function
    | [] | [_] -> []
    | x :: tail -> List.map (fun y -> x, y) tail @ getCombinations tail

let getGalaxies = Array2D.foldi (fun x y list v ->
      if v = '#' then (x, y) :: list else list) []

let run multiplier universe =
   let abs = abs >> int64
   let multiplier = if multiplier > 1L then multiplier-1L else 1L

   let emptyRows =
      [| for x in 0..Array2D.length1 universe - 1 do
         if universe[x,*] |> Array.forall ((=) '.') then x |]
   let emptyCols =
      [| for y in 0..Array2D.length2 universe - 1 do
         if universe[*,y] |> Array.forall ((=) '.') then y |]

   let countSteps (x1,y1) (x2,y2) =
      let x = emptyRows |> Seq.filter (fun x -> x >=< (min x1 x2, max x1 x2)) |> Seq.length |> int64
      let y = emptyCols |> Seq.filter (fun y -> y >=< (min y1 y2, max y1 y2)) |> Seq.length |> int64
      (abs(x1-x2)+x*multiplier) + (abs(y1-y2)+y*multiplier)

   getGalaxies universe
   |> getCombinations
   |> List.sumBy ((<||) countSteps)

let runPartOne = parse >> run 1L

let runPartTwo = parse >> run 1_000_000L

