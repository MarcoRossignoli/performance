﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using BenchmarkDotNet.Attributes;

namespace System.MathBenchmarks
{
    public partial class Single
    {
        // Tests MathF.Pow(float, float) over 5000 iterations for the domain x: +2, +1; y: -2, -1

        private const float powDeltaX = -0.0004f;
        private const float powDeltaY = 0.0004f;
        private const float powExpectedResult = 4659.30762f;

        [Benchmark]
        public void Pow() => PowTest();

        public static void PowTest()
        {
            var result = 0.0f; var valueX = 2.0f; var valueY = -2.0f;

            for (var iteration = 0; iteration < MathTests.Iterations; iteration++)
            {
                valueX += powDeltaX; valueY += powDeltaY;
                result += MathF.Pow(valueX, valueY);
            }

            var diff = MathF.Abs(powExpectedResult - result);

            if (diff > MathTests.SingleEpsilon)
            {
                throw new Exception($"Expected Result {powExpectedResult,10:g9}; Actual Result {result,10:g9}");
            }
        }
    }
}
