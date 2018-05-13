#r "paket:
    nuget Xake ~> 1.1 prerelease
    nuget Xake.Dotnet ~> 1.1 prerelease //"

#if !FAKE
  #load ".fake/hello-fwk2-4.fsx/intellisense.fsx"
#endif

// this script uses mono to build assembly
// see hwmono.cmd for more details

open Xake
open Xake.Dotnet

do xakeScript {
  var "NETFX" "mono-35"
  want ["out//hwmono.exe"]
  rules [
    "hwmono.exe" ..> csc {src !! "a.cs"}
  ]
}
