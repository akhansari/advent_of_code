module Day12

open System
open System.Collections.Generic

let isEdge (region: Set<_>) pos dir =
    Point.nextPos pos dir |> region.Contains |> not

let isCorner (region: Set<_>) (p1, p2, p3) =
    // 90° or 270°
    (region.Contains p1 && not (region.Contains p2) && region.Contains p3) ||
    not (region.Contains p1 || region.Contains p3)

let calculatePerimeter region =
    (0, region)
    ||> Set.fold (fun count p ->
        Point.FourDirections
        |> List.sumBy (isEdge region p >> Convert.ToInt32)
        |> (+) count)

let calculateSides (region: Set<_>) =

    let countCorners (x, y) =
        [| ((x, y - 1), (x - 1, y - 1), (x - 1, y))    // top left
           ((x - 1, y), (x - 1, y + 1), (x, y + 1))    // top right
           ((x, y + 1), (x + 1, y + 1), (x + 1, y))    // bottom right
           ((x + 1, y), (x + 1, y - 1), (x, y - 1)) |] // bottom left
        |> Array.sumBy (isCorner region >> Convert.ToInt32)

    region |> Seq.sumBy countCorners

let discoverRegions plan =
    let visited = HashSet()

    let discoverRegion start plant =
        let region = ResizeArray()

        let rec dfs pos =
            if Array2D.inBound pos plan
                && plan.Get pos = plant
                && visited.Add pos
            then
                region.Add pos
                for dir in Point.FourDirections do
                    Point.nextPos pos dir |> dfs

        dfs start
        Set.ofSeq region

    (List.empty, plan)
    ||> Array2D.foldi (fun x y c regions ->
        if visited.Contains (x, y)
        then regions
        else (discoverRegion (x, y) c) :: regions)

let runPartOne input =
    Array2D.fromString input
    |> discoverRegions
    |> List.sumBy (fun region ->
        let area = Set.count region
        let perimeter = calculatePerimeter region
        area * perimeter)

let runPartTwo input =
    Array2D.fromString input
    |> discoverRegions
    |> List.sumBy (fun region ->
        let area = Set.count region
        let sides = calculateSides region
        area * sides)

(*
Consider the input as a graph. Then with depth-first search, extract all regions into sets.
- Count all external edges
- Count all sides by counting all external corners
*)
