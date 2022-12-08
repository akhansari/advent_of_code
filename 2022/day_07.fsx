open System

let input = "$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k"

let (|Command|_|) (str: string array) =
    if str[0] = "$" then Array.skip 1 str |> Some else None

let (|File|_|) (str: string array) =
    match Int64.TryParse str[0] with
    | true, size -> Some size | _ -> None

let folderSizes (lines: string seq) =
    use enum = lines.GetEnumerator()
    let sizes = ResizeArray()
    let rec addUp () =
        let mutable sum = 0L
        let mutable loop = true
        while loop && enum.MoveNext() do
            match enum.Current.Split ' ' with
            | Command [| "cd"; "/" |] ->
                ()
            | Command [| "cd"; ".." |] ->
                loop <- false
            | Command [| "cd"; _ |] ->
                let size = addUp ()
                sum <- sum + size
            | File size ->
                sum <- sum + size
            | _ ->
                ()
        sizes.Add sum
        sum
    addUp() |> ignore
    sizes

let sizes = input.Split '\n' |> folderSizes
// Part One
sizes |> Seq.filter (fun i -> i < 100_000L) |> Seq.sum |> printfn "%A"
// Part Two
let free = 70_000_000L - (Seq.last sizes)
sizes |> Seq.filter (fun i -> i + free >= 30_000_000L) |> Seq.min |> printfn "%A"
