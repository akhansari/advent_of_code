module Day15.Tests

open Xunit

let sample =
    "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"

[<Fact>]
let ``Compute hash`` () =
    52 =! (hashOf "HASH")

[<Fact>]
let ``Part one`` () =
    1320 =! (sample |> runPartOne)

[<Fact>]
let ``Part two`` () =
    145 =! (sample |> runPartTwo)
