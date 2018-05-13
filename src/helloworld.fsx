#r "paket:
    nuget Xake ~> 1.1 prerelease
    nuget Xake.Dotnet ~> 1.1 prerelease //"

#if !FAKE
  #load ".fake/helloworld.fsx/intellisense.fsx"
#endif

// this script demonstrates most simple scenario

open Xake
open Xake.Dotnet

do xakeScript {
  want ["out/hw.exe"]
  filelog "helloworld.log" Chatty

  rule ("out/hw.exe" ..> csc {src !! "helloworld.cs"})
}
