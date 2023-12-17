let run () =
    use _ = measureElapsedTime ()
    loadText 13 |> Day13.runPartTwo |> printfn "%A"
run ()
