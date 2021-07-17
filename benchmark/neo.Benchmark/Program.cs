using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using neo.Benchmark.benchmark;

namespace neo.Benchmark
{
    class MainClass
    {
        public static void Main(string[] args)
        {
           var summar = BenchmarkRunner.Run<BM_Instruction>();
            //BenchmarkRunner.Run<BM_BigInteger>();
        }
    }
}

