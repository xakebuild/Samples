#r "paket:
    nuget Xake ~> 1.1 prerelease
    nuget Xake.Dotnet ~> 1.1 prerelease //"

#if !FAKE
#load ".fake/parallel.fsx/intellisense.fsx"
#endif

// this script demonstrates parallel execution of multiple targets
// >fake parallel.fsx -- clean build

open Xake
open Xake.Tasks
open Xake.Dotnet

do xake {ExecOptions.Default with  Threads = 4 } {

  filelog "parallel.log" Verbosity.Chatty
  want ["build"]

  rules [
    "clean" ..> rm {file ("out" </> "a*.exe")}
    "build" <== ([1..20] |> List.map (sprintf "out/a%i.exe"))
    "out" </> "*.exe" ..> csc {src !! "helloworld.cs"}
  ]
}
