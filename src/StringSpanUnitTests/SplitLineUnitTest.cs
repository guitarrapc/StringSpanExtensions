using System;
using Xunit;
using StringSpan;
using System.Collections.Generic;
using System.Linq;

namespace StringSpanUnitTests
{
    public class SplitLineUnitTest
    {
        const string Value = "Nickname: meziantou\r\nFirstName: Gerald\nLastName: Barre";
        const string ValueEmpty = "Nickname: meziantou\r\n\nLastName: Barre";

        [Fact]
        public void SplitLineShouldBeSameToSplit()
        {
            var list = Value;
            var expected = list.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToArray();

            var actual = new List<string>();
            foreach (ReadOnlySpan<char> item in list.SplitLines())
            {
                actual.Add(item.ToString());
            }

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(actual[i], expected[i]);
            }
        }

        [Fact]
        public void SplitLineRemoveEmptyEntryShouldSameToSplit()
        {
            var list = ValueEmpty;
            var expected = list.Split(new [] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var actual = new List<string>();
            foreach (ReadOnlySpan<char> item in list.SplitLines(StringSplitOptions.RemoveEmptyEntries))
            {
                actual.Add(item.ToString());
            }

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(actual[i], expected[i]);
            }
        }

        [Fact]
        public void SplitLineNoneShouldSameToSplit()
        {
            var list = ValueEmpty;
            var expected = list.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToArray();

            var actual = new List<string>();
            foreach (ReadOnlySpan<char> item in list.SplitLines(StringSplitOptions.None))
            {
                actual.Add(item.ToString());
            }

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(actual[i], expected[i]);
            }
        }
    }
}
