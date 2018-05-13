#r "paket:
    nuget Xake ~> 1.1 prerelease
    nuget Xake.Dotnet ~> 1.1 prerelease //"

#if !FAKE
#load ".fake/msbuild.fsx/intellisense.fsx"
#endif

open Xake
open Xake.Dotnet

do xakeScript {
    rules [
        "main" => MSBuild {
            MSBuildSettings with
                BuildFile = "HelloWorldSln\\Hello.sln"
                Target = ["Clean"; "Rebuild"]
                Property = [("OutDir", "../out")]
            }
    ]
}
