open System
open System.Collections.Generic

let input = "30373
25512
65332
33549
35390"

let parse (data: string) =
    data.Split '\n'
    |> Array.map (fun l -> l.ToCharArray())

let (>=<) i (min, max) = (i >= min) && (i <= max)

let findVisibleFromBottom (forest: char[][]) (my, mx) (sy, sx) =
    seq {
        let mutable baseline = '0'
        let mutable y = sy
        let mutable x = sx
        while y >=< (0, forest.Length-1) && x >=< (0, forest[y].Length-1) do
            if forest[y][x] > baseline then
                baseline <- forest[y][x]
                yield (y, x)
            y <- y + my
            x <- x + mx
    }

let countVisibleInteriorTrees (forest: char[][]) =
    let hs = HashSet<int * int>()
    let height = forest.Length
    let width = forest[0].Length
    let find direction start =
        findVisibleFromBottom forest direction start
        |> Seq.filter (fun (y,x) -> y >=< (1, height-2) && x >=< (1, width-2))
        |> Seq.iter (hs.Add >> ignore)
    for y = 1 to height-2 do
        find (0, 1) (y,0)
        find (0,-1) (y,width-1)
    for x = 1 to width-2 do
        find ( 1,0) (0,x)
        find (-1,0) (height-1,x)
    hs.Count

let countForestVisibleTrees (forest: char[][]) =
    let interiorTrees = countVisibleInteriorTrees forest
    let edgeTrees = ((forest.Length - 1) * 2) + ((forest[0].Length - 1) * 2)
    interiorTrees + edgeTrees

let countVisibleFromTop (forest: char[][]) (sy, sx) (my, mx) =
    seq {
        let mutable y = sy + my
        let mutable x = sx + mx
        let mutable loop = true
        while loop && y >=< (0, forest.Length-1) && x >=< (0, forest[y].Length-1) do
            yield (y,x)
            if forest[y][x] >= forest[sy][sx] then
                loop <- false
            y <- y + my
            x <- x + mx
    }
    |> Seq.length

let highestScenicScore (forest: char[][]) =
    seq {
        for y = 0 to forest.Length-1 do
            for x = 0 to forest[y].Length-1 do
                [ (0,1); (0,-1); (1,0); (-1,0) ]
                |> Seq.map (countVisibleFromTop forest (y,x))
                |> Seq.fold (*) 1
    }
    |> Seq.max

// Part One
input |> parse |> countForestVisibleTrees |> printfn "%A"
// Part Two
input |> parse |> highestScenicScore |> printfn "%A"
