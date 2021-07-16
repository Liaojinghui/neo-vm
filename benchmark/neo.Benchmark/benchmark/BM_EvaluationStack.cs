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
    public class BM_EvaluationStack
    {

        EvaluationStack stack = null;

        //private static EvaluationStack CreateOrderedStack(int count)
        //{
        //    var check = new Integer[count];
        //    EvaluationStack stack = new EvaluationStack(new ReferenceCounter());

        //    for (int x = 1; x <= count; x++)
        //    {
        //        stack.Push(x);
        //        check[x - 1] = x;
        //    }

        //    Assert.AreEqual(count, stack.Count);
        //    CollectionAssert.AreEqual(check, stack.ToArray());

        //    return stack;
        //}

        //public static IEnumerable GetEnumerable(IEnumerator enumerator)
        //{
        //    while (enumerator.MoveNext()) yield return enumerator.Current;
        //}


        //[GlobalSetup]
        //public void Setup()
        //{
        //    stack = CreateOrderedStack(3);
        //}


        //[Benchmark]
        //public void Stack_Pop() => stack.Pop<Integer>();

        //[Benchmark]
        //public void Stack_Peek() => stack.Peek(-1);

        //[Benchmark]
        //public void Stack_push() => stack.Push(1);
    }
}
