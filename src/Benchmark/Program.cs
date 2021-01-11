using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using StringSpan;
using BenchmarkDotNet.Configs;

namespace Benchmark
{
    class Program
    {
        public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }

    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [MemoryDiagnoser]
    public class SubstringBenchmark
    {
        const string Value = "asdfadsfadfadfadfadsfadsfadsfadsfdsfadfasdfasdfa";

        [BenchmarkCategory("Substring")]
        [Benchmark]
        public void Substring()
        {
            Value.Substring(10);
        }

        [BenchmarkCategory("Substring")]
        [Benchmark]
        public void Span()
        {
            Value.SubstringSpan(10);
        }
    }

    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [MemoryDiagnoser]
    public class SplitLineBenchmark
    {
        const string Value = "Nickname: meziantou\r\nFirstName: Gerald\nLastName: Barre";

        [BenchmarkCategory("SplitLine")]
        [Benchmark]
        public void StringReader()
        {
            var reader = new System.IO.StringReader(Value);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
            }
        }

        [BenchmarkCategory("SplitLine")]
        [Benchmark]
        public void Split()
        {
            foreach (var line in Value.Split(new [] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
            }
        }

        [BenchmarkCategory("SplitLine")]
        [Benchmark]
        public void Span()
        {
            foreach (ReadOnlySpan<char> item in Value.SplitLines())
            {
            }
        }
    }

    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [MemoryDiagnoser]
    public class SplitBenchmark
    {
        const string Value = "Nickname: meziantou,FirstName: Gerald,LastName: Barre";

        [BenchmarkCategory("Split")]
        [Benchmark]
        public void Split()
        {
            foreach (var item in Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
            }
        }

        [BenchmarkCategory("Split")]
        [Benchmark]
        public void Span()
        {
            foreach (ReadOnlySpan<char> item in Value.SplitSpan(','))
            {
            }
        }
    }
}
