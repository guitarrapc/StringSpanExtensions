using System;
using System.Collections.Generic;
using System.Text;

namespace StringSpan
{
    public static class StringSpanLineSplitExtensions
    {
        public static LineSplitEnumerator SplitLines(this string str)
        {
            return new LineSplitEnumerator(str.AsSpan(), StringSplitOptions.None);
        }

        public static LineSplitEnumerator SplitLines(this string str, StringSplitOptions options)
        {
            return new LineSplitEnumerator(str.AsSpan(), options);
        }

        // Must be a ref struct as it contains a ReadOnlySpan<char>
        public ref struct LineSplitEnumerator
        {
            private ReadOnlySpan<char> _str;
            private readonly StringSplitOptions _options;

            public LineSplitEnumerator(ReadOnlySpan<char> str, StringSplitOptions options)
            {
                _str = str;
                _options = options;
                Current = default;
            }

            // foreach compatible
            public LineSplitEnumerator GetEnumerator() => this;
            public bool MoveNext()
            {
                var span = _str;
                if (span.Length == 0) // Reach the end of the string
                    return false;

                var index = span.IndexOfAny('\r', '\n');
                // "HIJKLMN"
                if (index == -1)
                {
                    // single line
                    _str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
                    Current = new LineSplitEntry(span, ReadOnlySpan<char>.Empty);
                    return true;
                }

                // "ABCDEFG\r\nHIJKLMN"
                if (index < span.Length - 1 && span[index] == '\r')
                {
                    // Try to consume the '\n' associated to the '\r'
                    var next = span[index + 1];
                    if (next == '\n')
                    {
                        Current = new LineSplitEntry(span.Slice(0, index), span.Slice(index, 2));
                        _str = span.Slice(index + 2);
                        return true;
                    }
                }

                if (_options == StringSplitOptions.RemoveEmptyEntries && index < span.Length - 1 && span[index] == '\n')
                {
                    Current = new LineSplitEntry(span.Slice(1, index == 0 ? 0 : index - 1), span.Slice(index, 1));
                    _str = span.Slice(index + 1);
                    MoveNext();
                    return true;
                }

                Current = new LineSplitEntry(span.Slice(0, index), span.Slice(index, 1));
                _str = span.Slice(index + 1);
                return true;
            }
            public LineSplitEntry Current { get; private set; }
        }

        public readonly ref struct LineSplitEntry
        {
            public LineSplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator)
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

            // implicit cast to ReadOnlySpan<char>
            // foreach (ReadOnlySpan<char> entry in str.SplitLines())
            public static implicit operator ReadOnlySpan<char>(LineSplitEntry entry) => entry.Line;
        }
    }
}
