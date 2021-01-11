using System;
using System.Collections.Generic;
using System.Text;

namespace StringSpan
{
    public static class StringSpanSplitExtensions
    {
        public static SplitEnumerator SplitSpan(this string str, char separator)
        {
            return new SplitEnumerator(str.AsSpan(), separator);
        }

        // Must be a ref struct as it contains a ReadOnlySpan<char>
        public ref struct SplitEnumerator
        {
            private ReadOnlySpan<char> _str;
            private char _separator;

            public SplitEnumerator(ReadOnlySpan<char> str, char separator)
            {
                _str = str;
                _separator = separator;
                Current = default;
            }

            // foreach compatible
            public SplitEnumerator GetEnumerator() => this;
            public bool MoveNext()
            {
                var span = _str;
                if (span.Length == 0) // Reach the end of the string
                    return false;

                var index = span.IndexOf(_separator);
                if (index == -1)
                {
                    // not exists
                    _str = ReadOnlySpan<char>.Empty;
                    Current = new SplitEntry(span, ReadOnlySpan<char>.Empty);
                    return true;
                }

                Current = new SplitEntry(span.Slice(0, index), span.Slice(index, 1));
                _str = span.Slice(index + 1);
                return true;
            }
            public SplitEntry Current { get; private set; }
        }

        public readonly ref struct SplitEntry
        {
            public SplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator)
            {
                Line = line;
                Separator = separator;
            }

            public ReadOnlySpan<char> Line { get; }
            public ReadOnlySpan<char> Separator { get; }

            public void Deconstruct(out ReadOnlySpan<char> line, out ReadOnlySpan<char> separator)
            {
                line = Line;
                separator = Separator;
            }

            // This method allow to implicitly cast the type into a ReadOnlySpan<char>, so you can write the following code
            // foreach (ReadOnlySpan<char> entry in str.SplitLines())
            public static implicit operator ReadOnlySpan<char>(SplitEntry entry) => entry.Line;
        }
    }
}
