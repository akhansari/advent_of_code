let input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb
bvwbjplbgvbhsrlpgdmjqwftvncz
nppdvjthqldpwncqszvftbrmjlhg
nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg
zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"

let findMarker length (signal: string) =
    let hs = System.Collections.Generic.HashSet()
    let rec find i =
        if Seq.exists (hs.Add >> not) signal[i..i+length-1] then
            hs.Clear()
            find (i + 1)
        else
            i + length
    find 0

let data = input.Split '\n'
data |> Array.map (findMarker  4) // Part One
data |> Array.map (findMarker 14) // Part Two
