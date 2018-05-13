#r "paket:
    nuget Xake ~> 1.1 prerelease
    nuget Xake.Dotnet ~> 1.1 prerelease //"

#if !FAKE
  #load ".fake/project-example.fsx/intellisense.fsx"
#endif

(*

This script demonstrates the project consisting of two assemblies
notice the dependencies are built automatically

> fake project-example.fsx -- -d VER=1.21.0

Run the script twice and notice the following line in the console output:

  `[CMD] 2> Skipped ... \xakebuild\samples\src\out\hw.dll (up to date)`

which indicated that Xake figured out that output will not change.
After first and subsequent runs Xake stores dependencies to build database (.xake file). In this
example the tool analyzed the following dependencies chain:

Variable VER -> ver.cs -> hw.exe

and omitted first two build steps.

Now change the version and run script again:
> fake project-example.fsx -- -d VER=1.21.1

Notice Xake rebuilt ver.cs and hw.exe this time

*)

open Xake
open Xake.Tasks
open Xake.Dotnet

let srcPath = "project-example-src"

do xakeScript {
    rules [
        "main" <== ["out/hw.exe"]
        "out/hw.dll" ..> csc {src (!! "util.cs" @@ srcPath)}

        "out/hw.exe" ..> csc {
            target TargetType.Exe
            src (!! "hw.cs" ++ "ver.cs" @@ srcPath)
            ref !! "out/hw.dll"
        }

        srcPath </> "ver.cs" ..> recipe {
            let! envver = getVar "VER"
            let ver = envver |> Option.defaultValue "v0.1"
            do! trace Message "Updating version number to `%s`" ver
            do! writeText (sprintf """// static class App {const string Ver = "%s";}""" ver)
        }
    ]
}
