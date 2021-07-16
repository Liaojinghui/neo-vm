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
        private readonly bool a = true;
        private readonly bool b = false;


        [GlobalSetup]
        public void Setup()
        {
            //stack = CreateOrderedStack(3);
        }


        [Benchmark]
        public bool Instruction_BOOLAND() => a && b;


        [Benchmark]
        public bool Instruction_BOOLAND_Bit() => a & b;

        [Benchmark]
        public bool Instruction_BOOLOR() => a || b;


        [Benchmark]
        public bool Instruction_BOOLOR_Bit() => a | b;



    }
}
