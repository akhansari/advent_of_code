module Day05.Tests

open Xunit

let sample =
    "seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4"

// [<Fact>]
// let ``Seed path`` () =
//     let _, allMappings = parse sample
//     let path = seedPath allMappings 79
//     [|79L;81;81;81;74;78;78;82|] =! path

[<Fact>]
let ``Get the location`` () =
    let _, allMappings = parse sample
    let loc = locationOf allMappings 79
    82L =! loc

[<Fact>]
let ``Part one`` () =
    35L =! (sample |> runPartOne)

[<Fact>]
let ``Part two`` () =
    46L =! (sample |> runPartTwo)

