module Day09Tests

open Xunit
open Day09

let input = "2333133121414131402"

[<Fact>]
let ``test part 1`` () = input |> runPartOne =! 1928

[<Fact>]
let ``test part 2`` () = input |> runPartTwo =! 2858
