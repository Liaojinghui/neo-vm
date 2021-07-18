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
using Neo.VM.ObjectPool;
namespace neo.Benchmark.benchmark
{
     [DryJob]
    public class BM_Instruction
    {
        //private readonly bool a = true;
        //private readonly bool b = false;
        private  ScriptBuilder script = new ScriptBuilder();
        private ExecutionEngine engine = new ExecutionEngine();

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
             engine = new ExecutionEngine();
             script = new ScriptBuilder();
            engine.LoadScript(script.ToArray());
            Integer a = new Integer(10);
            Integer b = new Integer(10);
            engine.Push(a);
            engine.Push(b);

            yield return engine;
        }

        public IEnumerable<object[]> IntegerPool()
        {
            var pool = new ObjectPool<Integer>(() => new Integer(0));
            pool.Allocate(10);

            engine = new ExecutionEngine();
            script = new ScriptBuilder();
            engine.LoadScript(script.ToArray());
            Integer a = new Integer(10);
            Integer b = new Integer(10);
            engine.Push(a);
            engine.Push(b);

            yield return new object[]{engine, pool};

        }


        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_OR(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = engine.Pop().GetInteger();

        //    engine.Push(a | b);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_OR_Reuse(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = (Integer)engine.Pop();
        //    b.Value = a | b.GetInteger();
        //    engine.Push(b);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_AND(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = engine.Pop().GetInteger();

        //    engine.Push(a & b);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_AND_Reuse(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = (Integer)engine.Pop();
        //    b.Value = a & b.GetInteger();
        //    engine.Push(b);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_XOR(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = engine.Pop().GetInteger();

        //    engine.Push(a ^ b);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_XOR_Reuse(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = (Integer)engine.Pop();
        //    b.Value = a ^ b.GetInteger();
        //    engine.Push(b);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_MIN(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = engine.Pop().GetInteger();

        //    engine.Push(BigInteger.Min(a, b));
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_MIN_Reuse(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = (Integer)engine.Pop();
        //    b.Value = BigInteger.Min(a, b.GetInteger());
        //    engine.Push(b);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_MAX(ExecutionEngine engine)
        //{
        //    var a = engine.Pop().GetInteger();
        //    var b = engine.Pop().GetInteger();
        //    engine.Push(BigInteger.Max(a, b));
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_MAX_Reuse(ExecutionEngine engine)
        //{

        //    var a = engine.Pop().GetInteger();
        //    var b = (Integer)engine.Pop();
        //    b.Value = BigInteger.Max(a, b.GetInteger());
        //    engine.Push(b);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_POW(ExecutionEngine engine)
        //{
        //    var a = (int)engine.Pop().GetInteger();
        //    engine.Limits.AssertShift(a);
        //    var b = engine.Pop().GetInteger();

        //    engine.Push(BigInteger.Pow(b, a));
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_POW_Reuse(ExecutionEngine engine)
        //{
        //    var a = (int)engine.Pop().GetInteger();
        //    engine.Limits.AssertShift(a);
        //    var b = (Integer)engine.Pop();
        //    b.Value = BigInteger.Pow(b.GetInteger(), a);
        //    engine.Push(b);
        //}

        [Benchmark]
        [ArgumentsSource(nameof(IntegerPool))]
        public void Instruction_PUSH_Int(ExecutionEngine engine, ObjectPool<Integer> pool)
        {
            engine.Push(1000);
        }

        [Benchmark]
        [ArgumentsSource(nameof(IntegerPool))]
        public void Instruction_PUSH_Int_Pool(ExecutionEngine engine, ObjectPool<Integer> pool)
        {
            var a = pool.Dequeue();
            a.Value = 1000;
            engine.Push(a);
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
