using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.VM;

namespace Neo.Test
{
    [TestClass]
    public class UtDebugger
    {

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

        [TestMethod]
        public void TestBreakPoint()
        {
            using ExecutionEngine engine = new();
            using ScriptBuilder script = new();
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);

            engine.LoadScript(script.ToArray());

            Debugger debugger = new(engine);

            Assert.IsFalse(debugger.RemoveBreakPoint(engine.CurrentContext.Script, 3));
            Assert.IsFalse(debugger.RemoveBreakPoint(engine.CurrentContext.Script, 4));

            Assert.AreEqual(OpCode.NOP, engine.CurrentContext.NextInstruction.OpCode);

            debugger.AddBreakPoint(engine.CurrentContext.Script, 3);
            debugger.AddBreakPoint(engine.CurrentContext.Script, 4);
            debugger.Execute();

            Assert.AreEqual(OpCode.RET, engine.CurrentContext.NextInstruction.OpCode);
            Assert.AreEqual(3, engine.CurrentContext.InstructionPointer);
            Assert.AreEqual(VMState.BREAK, engine.State);

            Assert.IsTrue(debugger.RemoveBreakPoint(engine.CurrentContext.Script, 3));
            Assert.IsFalse(debugger.RemoveBreakPoint(engine.CurrentContext.Script, 3));
            Assert.IsTrue(debugger.RemoveBreakPoint(engine.CurrentContext.Script, 4));
            Assert.IsFalse(debugger.RemoveBreakPoint(engine.CurrentContext.Script, 4));
            debugger.Execute();

            Assert.AreEqual(VMState.HALT, engine.State);
        }

        [TestMethod]
        public void TestWithoutBreakPoints()
        {
            using ExecutionEngine engine = new();
            using ScriptBuilder script = new();
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);

            engine.LoadScript(script.ToArray());

            Debugger debugger = new(engine);

            Assert.AreEqual(OpCode.NOP, engine.CurrentContext.NextInstruction.OpCode);

            debugger.Execute();

            Assert.IsNull(engine.CurrentContext);
            Assert.AreEqual(VMState.HALT, engine.State);
        }

        [TestMethod]
        public void TestWithoutDebugger()
        {
            using ExecutionEngine engine = new();
            using ScriptBuilder script = new();
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);
            script.Emit(OpCode.NOP);

            engine.LoadScript(script.ToArray());

            Assert.AreEqual(OpCode.NOP, engine.CurrentContext.NextInstruction.OpCode);

            engine.Execute();

            Assert.IsNull(engine.CurrentContext);
            Assert.AreEqual(VMState.HALT, engine.State);
        }

        [TestMethod]
        public void TestStepOver()
        {
            using ExecutionEngine engine = new();
            using ScriptBuilder script = new();
            /* ┌     CALL 
               │  ┌> NOT
               │  │  RET
               └> │  PUSH0  
                └─┘  RET */
            script.EmitCall(4);
            script.Emit(OpCode.NOT);
            script.Emit(OpCode.RET);
            script.Emit(OpCode.PUSH0);
            script.Emit(OpCode.RET);

            engine.LoadScript(script.ToArray());

            Debugger debugger = new(engine);

            Assert.AreEqual(OpCode.NOT, engine.CurrentContext.NextInstruction.OpCode);
            Assert.AreEqual(VMState.BREAK, debugger.StepOver());
            Assert.AreEqual(2, engine.CurrentContext.InstructionPointer);
            Assert.AreEqual(VMState.BREAK, engine.State);
            Assert.AreEqual(OpCode.RET, engine.CurrentContext.NextInstruction.OpCode);

            debugger.Execute();

            Assert.AreEqual(true, engine.ResultStack.Pop().GetBoolean());
            Assert.AreEqual(VMState.HALT, engine.State);

            // Test step over again

            Assert.AreEqual(VMState.HALT, debugger.StepOver());
            Assert.AreEqual(VMState.HALT, engine.State);
        }

        [TestMethod]
        public void TestStepInto()
        {
            using ExecutionEngine engine = new();
            using ScriptBuilder script = new();
            /* ┌     CALL
               │  ┌> NOT 
               │  │  RET
               └> │  PUSH0
                └─┘  RET */
            script.EmitCall(4);
            script.Emit(OpCode.NOT);
            script.Emit(OpCode.RET);
            script.Emit(OpCode.PUSH0);
            script.Emit(OpCode.RET);

            engine.LoadScript(script.ToArray());

            Debugger debugger = new(engine);

            var context = engine.CurrentContext;

            Assert.AreEqual(context, engine.CurrentContext);
            Assert.AreEqual(context, engine.EntryContext);
            Assert.AreEqual(OpCode.NOT, engine.CurrentContext.NextInstruction.OpCode);

            Assert.AreEqual(VMState.BREAK, debugger.StepInto());

            Assert.AreNotEqual(context, engine.CurrentContext);
            Assert.AreEqual(context, engine.EntryContext);
            Assert.AreEqual(OpCode.RET, engine.CurrentContext.NextInstruction.OpCode);

            Assert.AreEqual(VMState.BREAK, debugger.StepInto());
            Assert.AreEqual(VMState.BREAK, debugger.StepInto());

            Assert.AreEqual(context, engine.CurrentContext);
            Assert.AreEqual(context, engine.EntryContext);
            Assert.AreEqual(OpCode.RET, engine.CurrentContext.NextInstruction.OpCode);

            Assert.AreEqual(VMState.BREAK, debugger.StepInto());
            Assert.AreEqual(VMState.HALT, debugger.StepInto());

            Assert.AreEqual(true, engine.ResultStack.Pop().GetBoolean());
            Assert.AreEqual(VMState.HALT, engine.State);

            // Test step into again

            Assert.AreEqual(VMState.HALT, debugger.StepInto());
            Assert.AreEqual(VMState.HALT, engine.State);
        }

        [TestMethod]
        public void TestBreakPointStepOver()
        {
            using ExecutionEngine engine = new();
            using ScriptBuilder script = new();
            /* ┌     CALL 
               │  ┌> NOT
               │  │  RET
               └>X│  PUSH0
                 └┘  RET */
            script.EmitCall(4);
            script.Emit(OpCode.NOT);
            script.Emit(OpCode.RET);
            script.Emit(OpCode.PUSH0);
            script.Emit(OpCode.RET);

            engine.LoadScript(script.ToArray());

            Debugger debugger = new(engine);

            Assert.AreEqual(OpCode.NOT, engine.CurrentContext.NextInstruction.OpCode);

            debugger.AddBreakPoint(engine.CurrentContext.Script, 5);
            Assert.AreEqual(VMState.BREAK, debugger.StepOver());

            Assert.AreEqual(OpCode.RET, engine.CurrentContext.NextInstruction.OpCode);
            Assert.AreEqual(5, engine.CurrentContext.InstructionPointer);
            Assert.AreEqual(VMState.BREAK, engine.State);

            debugger.Execute();

            Assert.AreEqual(true, engine.ResultStack.Pop().GetBoolean());
            Assert.AreEqual(VMState.HALT, engine.State);
        }

        [TestMethod]
        public void TestSlow()
        {
           var engine = new ExecutionEngine();
           engine.LoadScript(FromHexString("56010c0240014a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b4a8b01f80f8d0c0240008b11c001000460589d604a1f0c0b646573657269616c697a650c14c0ef39cee0e4e925c6c2a06a79e1440dd86fceac41627d5b52455824d149"));

        }
    }
}
