open System
open System.Reflection
open Microsoft.FSharp.Reflection

let getModule day =
    Assembly.GetExecutingAssembly().GetTypes()
    |> Array.find (fun t -> FSharpType.IsModule t && t.Name = $"Day%02i{day}")

let getMethod (dayModule: Type) partNum =
    match partNum with
    | "1" -> "runPartOne" | "2" -> "runPartTwo" | _ -> failwith "bad part num"
    |> dayModule.GetMethod

let getParamType (mi: MethodInfo) =
    let param = mi.GetParameters()
    if param.Length = 1 then
        param[0].ParameterType
    else
        failwith "bad function type"

[<EntryPoint>]
let main args =

    let day = int args[0]
    let dayModule = getModule day
    let mi = getMethod dayModule args[1]
    let pt = getParamType mi
    let print = printfn "%A"

    use _ = measureElapsedTime ()
    if pt = typeof<string> then
        mi.Invoke(dayModule, [| loadText day |> box |]) |> print
    elif pt = typeof<string array> then
        mi.Invoke(dayModule, [| loadLines day |> box |]) |> print
    else
        failwith "bad function type"

    0
