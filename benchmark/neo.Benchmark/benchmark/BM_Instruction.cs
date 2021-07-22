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
    [MemoryDiagnoser]
    public class BM_Instruction
    {
        //private readonly bool a = true;
        //private readonly bool b = false;
        //private ScriptBuilder script = new ScriptBuilder();
        private ExecutionEngine engine = new ExecutionEngine();

        private ExecutionEngine2 engine2= new ExecutionEngine2();
        /// <summary>
        /// Convert string in Hex format to byte array
        /// </summary>
        /// <param name="value">Hexadecimal string</param>
        ///// <returns>Return byte array</returns>
        public byte[] FromHexString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return System.Array.Empty<byte>();
            if (value.StartsWith("0x"))
                value = value[2..];
            if (value.Length % 2 == 1)
                throw new FormatException();

            var result = new byte[value.Length / 2];
            for (var i = 0; i < result.Length; i++)
                result[i] = byte.Parse(value.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier);

            return result;
        }

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


        //public IEnumerable<ExecutionEngine> NonPrimitive()
        //{
        //    engine = new ExecutionEngine();
        //    script = new ScriptBuilder();
        //    engine.LoadScript(FromHexString("c2104d11c0114d11c0560101fe019d60114d114d12c05312c0584a24f3455145"));
        //    //Integer a = new Integer(10);
        //    //Integer b = new Integer(10);
        //    _=PoolManager.Instance;
        //    //engine.Push(a);
        //    //engine.Push(b);

        //    yield return engine;
        //}


        public IEnumerable<object[]> IntegerPool()
        {

            engine = new ExecutionEngine();
             engine2 = new ExecutionEngine2();
            engine2.LoadScript(FromHexString("56010c0240014a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b01f80f8d0c0240008b11c001000460589d604a1f0c0b646573657269616c697a650c14c0ef39cee0e4e925c6c2a06a79e1440dd86fceac41627d5b52455824d149"));
            engine.LoadScript(FromHexString("56010c0240014a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b01f80f8d0c0240008b11c001000460589d604a1f0c0b646573657269616c697a650c14c0ef39cee0e4e925c6c2a06a79e1440dd86fceac41627d5b52455824d149"));

            _ = PoolManager.Instance;

            yield return new object[] { engine, engine2 };

        }


        //[Benchmark]
        //[ArgumentsSource(nameof(NonPrimitive))]
        //public void Instruction_OR(ExecutionEngine engine)
        //{
        //    for (int i = 0; i < 1000000; i++)
        //    {

        //        engine.Push(i ^ 20);
        //        var a = engine.Pop();
        //        var b = engine.Pop();

        //        engine.Push(a.GetInteger() | b.GetInteger());
        //    }
        //}

        [Benchmark]
        [ArgumentsSource(nameof(IntegerPool))]
        public void Instruction_OR(ExecutionEngine engine, ExecutionEngine2 engine2)
        {
            engine.Execute();
            //for (int i = 0; i < 1000000; i++)
            //{

            //    engine.Push(i ^ 20);
            //    var a = engine.Pop();
            //    var b = engine.Pop();

            //    engine.Push(a.GetInteger() | b.GetInteger());
            //}
        }

        [Benchmark]
        [ArgumentsSource(nameof(IntegerPool))]
        public void Instruction_OR_Pool(ExecutionEngine engine, ExecutionEngine2 engine2)
        {
            engine2.Execute();
            //for (int i = 0; i < 1000000; i++)
            //{
            //    //engine.Push(PoolManager.Instance.Reuse(i));
            //    engine.Push(PoolManager.Instance.Reuse(i ^ 20));
            //    var a = engine.Pop();
            //    var b = engine.Pop();
            //    PoolManager.Instance.Collect(a);
            //    PoolManager.Instance.Collect(b);
            //    engine.Push(PoolManager.Instance.Reuse(a.GetInteger() | b.GetInteger()));
            //}
        }

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

        //[Benchmark]
        //[ArgumentsSource(nameof(IntegerPool))]
        //public void Instruction_PUSH_Int(ExecutionEngine engine, ObjectPool<Integer> pool)
        //{
        //    engine.Push(1000);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(IntegerPool))]
        //public void Instruction_PUSH_Int_Pool(ExecutionEngine engine, ObjectPool<Integer> pool)
        //{
        //    var a = pool.Dequeue();
        //    a.Value = 1000;
        //    engine.Push(a);
        //}




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
