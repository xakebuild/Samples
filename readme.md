# Xake build tool samples

## Prerequisites

You will only need dotnet 2.0+ to run the samples.

## Running samples

Goto to src folder. Run the sample using `fake.cmd` batch file. E.g.

```
fake project-example.fsx
```

## List of samples

### helloworld.fsx

This script demonstrates most simple scenario, compiling .NET (Full framework) assembly.
This sample requires mono or Full .NET framework installed.

### msbuild.fsx

Using MSBuild task from `Xake.Dotnet` package to build solution file.
This sample requires mono or Full .NET framework installed.

### hello-csc6.fsx

Demonstrates using newest C# compiler to build Full .NET assemblies. Compiler tools areare downloaded via nuget. Use `hello-csc6.cmd` batch file to run the sample.

### hello-fwk2-4.fsx

Producing the binaries targeting different .NET runtimes.

### hwmono.fsx

Running tool using mono's `fsharpi` and using mono compiler to produce assemblies.

### multitarget.fsx

Defining the rule which produce multiple targets. See [Xake documentation](https://github.com/xakebuild/Xake/wiki/reference-%7C-rules#multitarget-rules) for details.

### parallel.fsx

Demonstrates running multiple independent targets in parallel to reduce the build time.

### project-example.fsx

Example of build script for project with two assemblies. Demonstrates dependency tracking.
Run the sample using the following command:

```
fake project-example.fsx -- -d VER=1.21.0
```

Run the command one more time and notice the following line in the console output:

```
  [CMD] 2> Skipped ... \xakebuild\samples\src\out\hw.dll (up to date)
```

which indicated that Xake figured out that output will not change. After first and subsequent runs Xake stores dependencies to build database (.xake file). In this example the tool analyzed the following dependency chain: `Variable VER -> ver.cs -> hw.exe` and omitted first two build steps.

Now change the version and run script again:

```
> fake project-example.fsx -- -d VER=1.21.1
```

Notice Xake rebuilt ver.cs and hw.exe this time.
