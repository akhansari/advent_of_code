module Day13

open System

let transpose (lines: string array) =
    lines |> Seq.transpose |> Seq.map String.Concat |> Seq.toArray

let getMirror (lines: string array) ia ib =
    let range = min ia (lines.Length-ib-1)
    let top = lines[ia-range..ia-1]
    let bottom = lines[ib+1..ib+range] |> Array.rev
    top, bottom

let findHorizontal lines =
    lines
    |> Array.indexed
    |> Array.pairwise
    |> Array.tryPick (fun ((ia, a), (ib, b)) ->
        if a = b then
            let top, bottom = getMirror lines ia ib
            if top = bottom then Some (ia+1) else None
        else
            None)

let find pattern =
    let lines = splitLines pattern
    findHorizontal lines
    |> Option.map ((*) 100)
    |> Option.defaultWith (fun () ->
        lines |> transpose |> findHorizontal |> Option.get)

let runPartOne content = split "\n\n" content |> Seq.sumBy find

let hasSmudge (a: string) (b: string) =
    let rec go i eq =
        if i < a.Length then
            match a[i] = b[i], eq with
            | true,  v     -> go (i+1) v
            | false, false -> go (i+1) true
            | false, true  -> false
        else
            eq
    go 0 false

let findHorizontalWithSmudge (lines: string array) =
    lines
    |> Array.indexed
    |> Array.pairwise
    |> Array.tryPick (fun ((ia, a), (ib, b)) ->

        let mutable smudgeFound = false
        let hasSmudge a b =
            if smudgeFound then false else
                smudgeFound <- hasSmudge a b
                smudgeFound
        let predicate a b = a = b || hasSmudge a b

        if predicate a b then
            let top, bottom = getMirror lines ia ib
            let isMirror = Array.forall2 predicate top bottom
            if isMirror && smudgeFound then Some (ia+1) else None
        else
            None)

let findWithSmudge pattern =
    let lines = splitLines pattern
    findHorizontalWithSmudge lines
    |> Option.map ((*) 100)
    |> Option.defaultWith (fun () ->
        lines |> transpose |> findHorizontalWithSmudge |> Option.get)

let runPartTwo content = split "\n\n" content |> Seq.sumBy findWithSmudge
