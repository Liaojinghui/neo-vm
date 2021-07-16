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
            BenchmarkRunner.Run<BM_EvaluationStack>();
            BenchmarkRunner.Run<BM_Instruction>();
        }
    }
}

