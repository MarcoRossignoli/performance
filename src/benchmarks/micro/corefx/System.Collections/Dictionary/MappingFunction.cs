﻿using BenchmarkDotNet.Attributes;
using MicroBenchmarks;
using System.Collections.Generic;

namespace System.Collections
{
    [BenchmarkCategory(Categories.CoreFX, Categories.Collections, Categories.GenericCollections)]
    public class DictionaryMappingFunction
    {
        [Params(500, 5_000, 50_000, 500_000, 5_000_000)]
        public int Items;

        [Benchmark]
        public void Entropy()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 1; i < Items; i++)
            {
                dict.Add(i, i);
                dict.Add(int.MinValue + i, int.MinValue + i);
            }

            for (int i = 1; i < Items; i++)
            {
                if (dict[i] != i)
                {
                    throw new Exception();
                }

                if (dict[int.MinValue + i] != int.MinValue + i)
                {
                    throw new Exception();
                }
            }

            for (int i = 1; i < Items; i++)
            {
                dict.Remove(i);
                dict.Remove(int.MinValue + i);
            }
        }
    }
}
