﻿module Program

open System
open System.IO
open System.Text
open Fake.IO
open Fake.Core

let path xs = Path.Combine(Array.ofList xs)

let solutionRoot = Files.findParent __SOURCE_DIRECTORY__ "Fable.Requests.sln";
let src = path [ solutionRoot; "src" ]
let tests = path [ solutionRoot; "tests" ]

let clean() =
    Shell.deleteDir (path [ src; "bin" ])
    Shell.deleteDir (path [ src; "obj" ])

let pack() =
    clean()
    if Shell.Exec("dotnet", "pack --configuration Release", src) <> 0 then
        failwith "Pack failed"
    else
        let outputPath = path [ src; "bin"; "Release" ]
        if Shell.Exec("dotnet", sprintf "tool install -g femto --add-source %s" outputPath) <> 0
        then failwith "Local install failed"

let publish() =
    clean()
    if Shell.Exec("dotnet", "pack --configuration Release", src) <> 0 then
        failwith "Pack failed"
    else
        let nugetKey =
            match Environment.environVarOrNone "NUGET_KEY" with
            | Some nugetKey -> nugetKey
            | None ->
                Console.WriteLine "NUGET_KEY environmental variable was not found, please enter it manually"
                Console.Write "Nuget API key: "
                let keyFromInput = Console.ReadLine()
                if String.isNullOrWhiteSpace keyFromInput
                then failwith "The Nuget API key provided is empty."
                else keyFromInput

        let nugetPath =
            Directory.GetFiles(path [ src; "bin"; "Release" ])
            |> Seq.head
            |> Path.GetFullPath

        if Shell.Exec("dotnet", sprintf "nuget push %s -s nuget.org -k %s" nugetPath nugetKey, src) <> 0
        then failwith "Publish failed"

let build() =
    if Shell.Exec("dotnet", "build --configuration Release", solutionRoot) <> 0
    then failwith "build failed"

let run() =
    if Shell.Exec("dotnet", "fable --lang python", tests) <> 0  then 
        failwith "Compiling the project failed"
    else
        if Shell.Exec("poetry", "run python program.py", tests) <> 0 
        then failwith "Running the project failed"


[<EntryPoint>]
let main (args: string[]) =
    try
        match args with
        | [| |]
        | [| "build"   |] -> build()
        | [| "pack"    |] -> pack()
        | [| "publish" |] -> publish()
        | [| "run" |] -> run()
        | _ -> printfn "Unknown args %A" args
        0
    with ex ->
        printfn $"{ex.Message}"
        1