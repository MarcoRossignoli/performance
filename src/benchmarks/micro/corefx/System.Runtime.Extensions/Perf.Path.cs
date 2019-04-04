// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

namespace System.IO.Tests
{
    [BenchmarkCategory(Categories.CoreFX)]
    public class Perf_Path
    {
        private readonly string _testPath = FileUtils.GetTestFilePath();
        private readonly string _testPath10 = PerfUtils.CreateString(10);
        private readonly string _testPath200 = PerfUtils.CreateString(200);
        private readonly string _testPath500 = PerfUtils.CreateString(500);
        private readonly string _testPath1000 = PerfUtils.CreateString(1000);

        [ParamsSource(nameof(DirectorySeparatorValues))]
        public string DirectorySeparator { get; set; }
        public System.Collections.Generic.IEnumerable<string> DirectorySeparatorValues => new[]  
        { 
            "path/" , @"path", "", null 
        };

        [Benchmark]
        public string Combine() => Path.Combine(_testPath, _testPath10);

        [Benchmark]
        public string GetFileName() => Path.GetFileName(_testPath);

        [Benchmark]
        public string GetDirectoryName() => Path.GetDirectoryName(_testPath);

        [Benchmark]
        public string ChangeExtension() => Path.ChangeExtension(_testPath, ".new");

        [Benchmark]
        public string GetExtension() => Path.GetExtension(_testPath);

        [Benchmark]
        public string GetFileNameWithoutExtension() => Path.GetFileNameWithoutExtension(_testPath);

        [Benchmark]
        public string GetFullPathForLegacyLength() => Path.GetFullPath(_testPath200);

#if !NETFRAMEWORK // long paths are always supported on .NET Core
        [Benchmark]
        public string GetFullPathForTypicalLongPath() => Path.GetFullPath(_testPath500);

        [Benchmark]
        public void GetFullPathForReallyLongPath() => Path.GetFullPath(_testPath1000);
#endif

        [Benchmark]
        public string GetPathRoot() => Path.GetPathRoot(_testPath);

        [Benchmark]
        public string GetRandomFileName() => Path.GetRandomFileName();

        [Benchmark]
        public string GetTempPath() => Path.GetTempPath();

        [Benchmark]
        public bool HasExtension() => Path.HasExtension(_testPath);

        [Benchmark]
        public bool IsPathRooted() => Path.IsPathRooted(_testPath);

        //[Benchmark]
        //public string TrimEndingDirectorySeparator_String() => Path.TrimEndingDirectorySeparator(DirectorySeparator);

        [Benchmark]
        public bool EndsInDirectorySeparator_String() => Path.EndsInDirectorySeparator(DirectorySeparator);
    }
}

/*
  C:\Users\Marco\Downloads\Tmp\PerfTrim\PerfTrim\PerfTrim\dotnet-sdk-latest-win-x64\dotnet.exe run -c release -f netcoreapp3.0 -- --filter *Perf_Path*DirectorySeparator* --coreRun c:\git\corefx\artifacts\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe 
 */
