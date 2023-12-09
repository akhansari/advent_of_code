module Day09

let differences = List.pairwise >> List.map (fun (a, b) -> b - a)
let rec arrange numbers =
    let diffSeq = differences numbers
    if List.forall ((=) 0) diffSeq
    then List.singleton numbers
    else numbers :: arrange diffSeq
let extrapolate = arrange >> List.sumBy List.last

let parse = Array.map (extractInt32 >> Seq.toList)
let runPartOne = parse >> Array.sumBy extrapolate
let runPartTwo = parse >> Array.map List.rev >> Array.sumBy extrapolate
