open System
open System.Collections.Generic

let input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb
bvwbjplbgvbhsrlpgdmjqwftvncz
nppdvjthqldpwncqszvftbrmjlhg
nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg
zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"

let rec private find length (span: ReadOnlySpan<char>) i =
    let hs = HashSet()
    if span.Slice(i, length).ToArray() |> Array.exists (hs.Add >> not)
    then find length span (i + 1)
    else i + length

let findMarker length (signal: string) =
    let span = signal.AsSpan()
    find length span 0

let data = input.Split '\n'
data |> Array.map (findMarker  4) |> printfn "%A" // Part One
data |> Array.map (findMarker 14) |> printfn "%A" // Part Two
