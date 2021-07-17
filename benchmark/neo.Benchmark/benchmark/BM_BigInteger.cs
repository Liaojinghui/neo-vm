using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.VM;
using Neo.VM.Types;
using System.Collections;
using System.Linq;
using System.Numerics;

namespace neo.Benchmark.benchmark
{
    public class BM_BigInteger
    {
        private readonly BigInteger a = ~2 ^ 32;
       


        [GlobalSetup]
        public void Setup()
        {
            //stack = CreateOrderedStack(3);
        }


        [Benchmark]
        public bool BigInteger_LT() => a < 0;


        [Benchmark]
        public bool BigInteger_Sign() => a.Sign < 0;

    }
}
