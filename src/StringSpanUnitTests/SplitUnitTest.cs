using System;
using Xunit;
using StringSpan;
using System.Collections.Generic;
using System.Linq;

namespace StringSpanUnitTests
{
    public class SplitUnitTest
    {
        const string Value = "Nickname: meziantou,FirstName: Gerald,LastName: Barre";

        [Fact]
        public void SpanSplitShouldBeSameToSplit()
        {
            var list = Value;
            var expected = list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var actual = new List<string>();
            foreach (ReadOnlySpan<char> item in Value.SplitSpan(','))
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
