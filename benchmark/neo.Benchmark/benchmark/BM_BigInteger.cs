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
        private readonly BigInteger a = ~2 ^ 8;
        byte[] data = null;

        public BM_BigInteger()
        {
            data = a.ToByteArray(isUnsigned: false, isBigEndian: false);
        }



        [Benchmark]
        public void BigInteger_PadRight() => ScriptBuilder.PadRight(data, 16, a.Sign < 0);


        //[Benchmark]
        //public bool BigInteger_Sign() => a.Sign < 0;

    }
}
