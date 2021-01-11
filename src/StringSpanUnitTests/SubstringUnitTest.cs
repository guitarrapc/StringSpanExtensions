using System;
using Xunit;
using StringSpan;
using System.Collections.Generic;
using System.Linq;

namespace StringSpanUnitTests
{
    public class SubstringUnitTest
    {
        const string Value = "asdfadsfadfadfadfadsfadsfadsfadsfdsfadfasdfasdfa";

        [Fact]
        public void SpanSubstringShouldBeSameToSplit()
        {
            var list = Value;
            var expected = list.Substring(10);
            var actual = list.SubstringSpan(10);
            Assert.Equal(expected, actual.ToString());
        }
    }
}
