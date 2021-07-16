using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.VM;
using Neo.VM.Types;
using System.Collections;
using System.Linq;

namespace neo.Benchmark.benchmark
{
    public class BM_Instruction
    {
        private readonly bool a = false;
        private readonly bool b = false;


        [GlobalSetup]
        public void Setup()
        {
            //stack = CreateOrderedStack(3);
        }


        [Benchmark]
        public bool Instruction_BOOLAND_And() =>a && b;


        [Benchmark]
        public bool Instruction_BOOLAND_Bit_And() => a & b;


    }
}
