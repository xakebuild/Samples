#r "paket:
    nuget Xake ~> 1.1 prerelease
    nuget Xake.Dotnet ~> 1.1 prerelease //"

#if !FAKE
#load ".fake/hello-csc6.fsx/intellisense.fsx"
#endif

// this script demonstrates using newest C# compiler to build Full .NET assemblies
// the sample relies on compiler tools downloaded via nuget. See command file:
// >hello-csc6.cmd

open Xake
open Xake.Tasks
open Xake.Dotnet

do xakeScript {
  
  var "NETFX-TARGET" "2.0"
  filelog "build.log" Logging.Diag
  want ["out/hello6.exe"]

  rules [
    "out/hello6.exe" ..> csc {
      cscpath @"packages\microsoft.net.compilers\2.8.0\tools\csc.exe"
      src !! "hello_cs6.cs"
    }
  ]
}
