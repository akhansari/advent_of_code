module Day13.Tests

open Xunit

let sample =
    "#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#"

[<Fact>]
let ``Part one, horizontal match`` () =
    400 =! ((split "\n\n" sample)[1] |> find)

[<Fact>]
let ``Part one, vertical match`` () =
    5 =! ((split "\n\n" sample)[0] |> find)

[<Fact>]
let ``Part one`` () =
    405 =! (sample |> runPartOne)

[<Fact>]
let ``Part two, first smudge`` () =
    300 =! ((split "\n\n" sample)[0] |> findWithSmudge)

[<Fact>]
let ``Part two, second smudge`` () =
    100 =! ((split "\n\n" sample)[1] |> findWithSmudge)

[<Fact>]
let ``Part two, finding a smudge is mandatory`` () =
    let sample =
        "##.####......
#####...##..#
.#..#.##...##
.#.#.##.#.#..
..##.#..#...#
#.########.#.
#.#######..#.
#.#######..#.
#.########.#."
    800 =! (sample |> findWithSmudge)

[<Fact>]
let ``Part two`` () =
    400 =! (sample |> runPartTwo)
