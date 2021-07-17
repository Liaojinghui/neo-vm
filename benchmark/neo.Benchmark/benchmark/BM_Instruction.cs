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

namespace neo.Benchmark.benchmark
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class BM_Instruction
    {
        private readonly bool a = true;
        private readonly bool b = false;
         ScriptBuilder script = new();
         ExecutionEngine engine = new();

        /// <summary>
        /// Convert string in Hex format to byte array
        /// </summary>
        /// <param name="value">Hexadecimal string</param>
        ///// <returns>Return byte array</returns>
        //public byte[] FromHexString( string value)
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
        public BM_Instruction()
        {
            script.Emit(OpCode.PUSH1);
            script.Emit(OpCode.PUSH1);
            script.Emit(OpCode.OR);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            //script.Emit(OpCode.PUSH1);
            engine.LoadScript(script.ToArray());
        }
        [Benchmark]
        public void Instruction_OR()
        {
       
            //engine.LoadScript(FromHexString("56010c0240014a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b01f80f8d0c0240008b11c001000460589d604a1f0c0b646573657269616c697a650c14c0ef39cee0e4e925c6c2a06a79e1440dd86fceac41627d5b52455824d149"));

            engine.Execute();
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
