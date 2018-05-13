#r "paket:
    nuget Xake ~> 1.1 prerelease //"

#if !FAKE
  #load ".fake/multitarget.fsx/intellisense.fsx"
#endif

// this script demonstrates defining the rules producing multiple targets
// see https://github.com/xakebuild/Xake/wiki/reference-%7C-rules#multitarget-rules for details

open Xake
open Xake.Tasks
open System.IO

do xakeScript {
    rules [
        "main" => recipe {
            do! alwaysRerun()
            // despite three call the recipe will be processed just once
            do! need ["out/b/app.txt"]
            do! need ["out/b/bin/app.exe"]
            do! need ["out/b/bin/app.xml"]
        }

        // group rule
        ["out/**/bin/*.exe"; "out/**/bin/*.xml"; "out/**/*.txt"] *..> recipe {
            let! mainTarget = getTargetFullName()
            let! [target1; target2; target3] = getTargetFiles()
            do! writeText "hello world"
            do! trace Message "main target is %A" mainTarget
            do File.WriteAllText(target1.FullName, "file1")
            do File.WriteAllText(target2.FullName, "file2")
            do File.WriteAllText(target3.FullName, "file3")
        }
    ]
}