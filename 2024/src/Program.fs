open System
open System.Reflection
open Microsoft.FSharp.Reflection

let getModule day =
    Assembly.GetExecutingAssembly().GetTypes()
    |> Array.find (fun t -> FSharpType.IsModule t && t.Name = $"Day%02i{day}")

let getMethod (dayModule: Type) partNum =
    match partNum with
    | "1" -> "runPartOne"
    | "2" -> "runPartTwo"
    | _ -> failwith "bad part num"
    |> dayModule.GetMethod

[<EntryPoint>]
let main args =

    if args.Length <> 2 then
        failwith "args: <day> <part number>"

    let day = int args[0]
    let dayModule = getModule day
    let methodInfo = getMethod dayModule args[1]
    let print = printfn "answer: %A"

    use _ = measureElapsedTime ()

    methodInfo.Invoke(dayModule, [| loadText day |> box |]) |> print

    0
