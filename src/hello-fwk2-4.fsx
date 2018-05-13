#r "paket:
    nuget Xake ~> 1.1 prerelease
    nuget Xake.Dotnet ~> 1.1 prerelease //"

#if !FAKE
  #load ".fake/hello-fwk2-4.fsx/intellisense.fsx"
#endif

// this script demonstrated how to define default target framework
// and how to define it per-target

open Xake
open Xake.Dotnet

do xakeScript {

  var "NETFX" "4.0"
  filelog "hello-fwk2-4.log" Chatty

  want ["out/hw2.exe"; "out/hw4.exe"]

  rules [

    "out/hw2.exe" ..> recipe {
        do! alwaysRerun()
        do! csc {
            targetfwk "2.0"
            src !!"helloworld.cs"
            grefs ["System.dll"]
            define ["TRACE"]
          }
        }
    "out/hw4.exe" ..> recipe {
        do! alwaysRerun()
        do! csc {
            // we do not define the targetfwk here so that globally defined one (see NETFX variable) will be used
            src !!"helloworld.cs"
            define ["TRACE"]
          }
        }]
}
