![dotnet-build](https://github.com/guitarrapc/StringSpanExtensions/workflows/dotnet-build/badge.svg)

## StringSpanExtensions

**Points**

* Converting `Span<char>` to `string` is allocated, so it's better treat Span<char> as it were.
  * If want to use `string` built-in methods are enough on `string.Substring`.

## Benchmark

|               Type |       Method |       Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |------------- |-----------:|----------:|----------:|-------:|------:|------:|----------:|
|     SplitBenchmark |        Split |  71.092 ns | 0.6563 ns | 0.6139 ns | 0.0153 |     - |     - |     256 B |
|     SplitBenchmark |         Span |  43.913 ns | 0.5738 ns | 0.4792 ns |      - |     - |     - |         - |
|                    |              |            |           |           |        |       |       |           |
| SplitLineBenchmark | StringReader |  54.256 ns | 0.6942 ns | 0.6494 ns | 0.0124 |     - |     - |     208 B |
| SplitLineBenchmark |        Split | 135.748 ns | 0.7827 ns | 0.7322 ns | 0.0157 |     - |     - |     264 B |
| SplitLineBenchmark |         Span |  46.315 ns | 0.2207 ns | 0.1723 ns |      - |     - |     - |         - |
|                    |              |            |           |           |        |       |       |           |
| SubstringBenchmark |    Substring |   7.730 ns | 0.0924 ns | 0.0772 ns | 0.0062 |     - |     - |     104 B |
| SubstringBenchmark |         Span |   1.679 ns | 0.0053 ns | 0.0047 ns |      - |     - |     - |         - |

## TODO

* [x] Substring: SubstringSpan -> `Span<char>`
* [x] Split: SplitSpan -> `Span<SplitEntry>`
* [x] Split for lines: SplitLines -> `Span<SplitLineEntry>`
