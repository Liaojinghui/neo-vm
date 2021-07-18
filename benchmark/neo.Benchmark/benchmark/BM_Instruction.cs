using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.VM;
using Neo.VM.Types;
using System.Collections;
using System.Linq;
using System.Globalization;
using BenchmarkDotNet.Jobs;
using System.Numerics;
using BenchmarkDotNet.Engines;
using System.Collections.Generic;

namespace neo.Benchmark.benchmark
{
    [DryJob]
    public class BM_Instruction
    {
        //private readonly bool a = true;
        //private readonly bool b = false;
        //private  ScriptBuilder script = new ScriptBuilder();
        //private  ExecutionEngine engine = new ExecutionEngine();

        /// <summary>
        /// Convert string in Hex format to byte array
        /// </summary>
        /// <param name="value">Hexadecimal string</param>
        ///// <returns>Return byte array</returns>
        //public byte[] FromHexString(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //        return System.Array.Empty<byte>();
        //    if (value.StartsWith("0x"))
        //        value = value[2..];
        //    if (value.Length % 2 == 1)
        //        throw new FormatException();

        //    var result = new byte[value.Length / 2];
        //    for (var i = 0; i < result.Length; i++)
        //        result[i] = byte.Parse(value.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier);

        //    return result;
        //}

        //[Params("2020202020")]
        //public String s;

        //[GlobalSetup]
        //public void Setup()
        //{
        //    engine = new ExecutionEngine();
        //    script = new ScriptBuilder();
        //    //    //script.Emit(OpCode.PUSH16);
        //    //    //script.Emit(OpCode.PUSH16);
        //    //    //script.Emit(OpCode.PUSH16);
        //    //    //script.Emit(OpCode.PUSH16);
        //    //    //script.Emit(OpCode.PUSH16);
        //    //    //script.Emit(OpCode.PUSH16);
        //    //    engine.LoadScript(script.ToArray());
        //    //    //engine.Push(10);
        //    //    //engine.Push(10);
        //    //    //engine.Push(10);
        //    //    //engine.Push(10);
        //    //    //engine.Push(10);
        //    //    //engine.Push(10);
        //}


        public IEnumerable<ExecutionEngine> NonPrimitive()
        {
            var engine = new ExecutionEngine();
            var script = new ScriptBuilder();
            engine.LoadScript(script.ToArray());
            Integer a = new Integer(10);
            Integer b = new Integer(10);
            engine.Push(a);
            engine.Push(b);

            yield return engine;
        }


        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_OR(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = engine.Pop().GetInteger();

            engine.Push(aa | bb);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_OR_Reuse(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = (Integer)engine.Pop();
            bb.Value = aa & bb.GetInteger();
            engine.Push(bb);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_AND(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = engine.Pop().GetInteger();

            engine.Push(aa | bb);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_AND_Reuse(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = (Integer)engine.Pop();
            bb.Value = aa & bb.GetInteger();
            engine.Push(bb);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_XOR(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = engine.Pop().GetInteger();

            engine.Push(aa ^ bb);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_XOR_Reuse(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = (Integer)engine.Pop();
            bb.Value = aa ^ bb.GetInteger();
            engine.Push(bb);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_MIN(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = engine.Pop().GetInteger();

            engine.Push(BigInteger.Min(aa, bb));
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_MIN_Reuse(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = (Integer)engine.Pop();
            bb.Value = BigInteger.Min(aa, bb.GetInteger());
            engine.Push(bb);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_MAX(ExecutionEngine engine)
        {
            var aa = engine.Pop().GetInteger();
            var bb = engine.Pop().GetInteger();
            engine.Push(BigInteger.Max(aa, bb));
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_MAX_Reuse(ExecutionEngine engine)
        {

            var aa = engine.Pop().GetInteger();
            var bb = (Integer)engine.Pop();
            bb.Value = BigInteger.Max(aa, bb.GetInteger());
            engine.Push(bb);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_POW(ExecutionEngine engine)
        {
            var aa = (int)engine.Pop().GetInteger();
            engine.Limits.AssertShift(aa);
            var bb = engine.Pop().GetInteger();

            engine.Push(BigInteger.Pow(bb, aa));
        }

        [Benchmark]
        [ArgumentsSource(nameof(NonPrimitive))]
        public void Instruction_POW_Reuse(ExecutionEngine engine)
        {
            var aa = (int)engine.Pop().GetInteger();
            engine.Limits.AssertShift(aa);
            var bb = (Integer)engine.Pop();
            bb.Value = BigInteger.Pow(bb.GetInteger(), aa);
            engine.Push(bb);
        }

        //[Benchmark]
        //public bool Instruction_BOOLAND() => a && b;


        //[Benchmark]
        //public bool Instruction_BOOLAND_Bit() => a & b;

        //[Benchmark]
        //public bool Instruction_BOOLOR() => a || b;


        //[Benchmark]
        //public bool Instruction_BOOLOR_Bit() => a | b;



    }
}
