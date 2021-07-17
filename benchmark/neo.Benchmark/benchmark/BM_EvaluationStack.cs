using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.VM;
using Neo.VM.Types;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace neo.Benchmark.benchmark
{
    public class BM_EvaluationStack
    {

        EvaluationStack stack = new EvaluationStack(new ReferenceCounter());


        public BM_EvaluationStack()
        {
            //stack = new EvaluationStack(new ReferenceCounter());
            
        }


        //[Benchmark]
        //public void Stack_push() { stack.Push(1); }

        [Benchmark]
        public void Stack_Pop()
        {
             //List<StackItem> innerList = new();
        StackItem s = (int)1;
            stack.Push(s);
            ////stack.Pop();
        }


        //[Benchmark]
        //public void Stack_Peek() => stack.Peek(-1);

    }
}
