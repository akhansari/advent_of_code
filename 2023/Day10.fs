module Day10

type Grid = char[,]
type Position = int * int
type Direction = East | West | South | North | Stop
type Tile = { Position: Position; Pipe: char; From: Direction; To: Direction }

let nextPosition (x,y) = function
    | East  -> (x,   y+1)
    | West  -> (x,   y-1)
    | South -> (x+1, y)
    | North -> (x-1, y)
    | Stop  -> (x,   y)

let nextDirection pipe from =
    match pipe, from with
    | '-', East  -> East
    | '-', West  -> West
    | '|', South -> South
    | '|', North -> North
    | 'L', South -> East
    | 'L', West  -> North
    | 'J', South -> West
    | 'J', East  -> North
    | '7', North -> West
    | '7', East  -> South
    | 'F', North -> East
    | 'F', West  -> South
    | _          -> Stop

let nextTile (grid: Grid) tile =
    let x,y = nextPosition tile.Position tile.To
    { Position = x,y
      Pipe = grid[x,y]
      From = tile.To
      To = nextDirection grid[x,y] tile.To }

let rec [<TailCall>] walk grid from path =
    let next = nextTile grid from
    if next.To = Stop
    then path
    else next :: walk grid next path

let findStartDirections grid =
    let startPos = Array2D.find 'S' grid |> Option.get
    [ East; West; South; North ]
    |> List.choose (fun dir ->
        match nextPosition startPos dir with
        | (x,y) when
            x >=< (0, Array2D.length1 grid) &&
            y >=< (0, Array2D.length2 grid) &&
            nextDirection grid[x,y] dir <> Stop
            -> Some { Position = startPos; Pipe = 'S'; From = dir; To = dir }
        | _ -> None)

let parse lines = lines |> Array.map Array.ofSeq |> array2D

let runPartOne lines =
    let grid = parse lines
    let start = findStartDirections grid |> List.head
    let loop = walk grid start [ start ]
    loop.Length / 2

// Pick's theorem
let countInteriorPoints boundaryPointsCount area  =
    area - (boundaryPointsCount / 2) + 1

// Shoelace formula
let areaOf vertices =
   let len = List.length vertices
   let mutable area = 0
   for i in 0..len-1 do
        let x1, y1 = vertices[i]
        let x2, y2 = vertices[(i + 1) % len]
        area <- area + x1 * y2 - x2 * y1
   abs area / 2

let runPartTwo lines =
    let grid = parse lines
    let start = findStartDirections grid |> List.head
    let loop = walk grid start [ start ]
    loop
    |> List.choose (fun tile ->
        if tile.Pipe <> '|' && tile.Pipe <> '-'
        then Some tile.Position
        else None)
    |> areaOf
    |> countInteriorPoints loop.Length
