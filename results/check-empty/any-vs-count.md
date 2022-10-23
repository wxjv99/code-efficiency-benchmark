``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.608)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores

```

``` ini
[Host] : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2

Runtime=.NET 6.0
```
|                    Method | Count |          Mean |     StdDev |  Ratio |
|-------------------------- |------ |--------------:|-----------:|-------:|
|         **IEnumerable.Any()** |   **100** |    **14.6601 ns** |  **0.2928 ns** |   **1.00** |
| &#39;IEnumerable.Count() &gt; 0&#39; |   100 |   208.9731 ns |  5.1278 ns |  14.32 |
|                List.Any() |   100 |     2.8993 ns |  0.0663 ns |   0.20 |
|        &#39;List.Count() &gt; 0&#39; |   100 |     3.2727 ns |  0.1832 ns |   0.23 |
|          &#39;List.Count &gt; 0&#39; |   100 |     0.1526 ns |  0.0130 ns |   0.01 |
|                           |       |               |            |        |
|         **IEnumerable.Any()** |  **1000** |    **15.2572 ns** |  **0.1342 ns** |   **1.00** |
| &#39;IEnumerable.Count() &gt; 0&#39; |  1000 | 1,854.3594 ns | 23.4600 ns | 121.35 |
|                List.Any() |  1000 |     3.3620 ns |  0.1428 ns |   0.22 |
|        &#39;List.Count() &gt; 0&#39; |  1000 |     3.1898 ns |  0.1212 ns |   0.21 |
|          &#39;List.Count &gt; 0&#39; |  1000 |     0.2403 ns |  0.0026 ns |   0.02 |


``` ini
[Host] : .NET Framework 4.8.1 (4.8.9075.0), X86 LegacyJIT

Runtime=.NET Framework 4.6.1
```
|                    Method | Count |          Mean |    StdDev |  Ratio |
|-------------------------- |------ |--------------:|----------:|-------:|
|         **IEnumerable.Any()** |   **100** |    **13.9918 ns** | **0.0478 ns** |   **1.00** |
| &#39;IEnumerable.Count() &gt; 0&#39; |   100 |   226.0391 ns | 2.7205 ns |  16.17 |
|                List.Any() |   100 |    18.0562 ns | 0.0302 ns |   1.29 |
|        &#39;List.Count() &gt; 0&#39; |   100 |     9.0207 ns | 0.1471 ns |   0.64 |
|          &#39;List.Count &gt; 0&#39; |   100 |     0.2090 ns | 0.0148 ns |   0.01 |
|                           |       |               |           |        |
|         **IEnumerable.Any()** |  **1000** |    **14.1522 ns** | **0.1638 ns** |   **1.00** |
| &#39;IEnumerable.Count() &gt; 0&#39; |  1000 | 2,045.8204 ns | 3.5717 ns | 144.50 |
|                List.Any() |  1000 |    18.0335 ns | 0.0300 ns |   1.27 |
|        &#39;List.Count() &gt; 0&#39; |  1000 |     8.9057 ns | 0.0283 ns |   0.63 |
|          &#39;List.Count &gt; 0&#39; |  1000 |     0.2159 ns | 0.0212 ns |   0.02 |


``` csharp
using BenchmarkDotNet.Attributes;
using CodeEfficiencyBenchmark.Common.Utils;
using System.Collections.Generic;
using System.Linq;

namespace CodeEfficiencyBenchmark.Benchmarks
{
    public class CheckEmpty
    {
        public IEnumerable<int> DataSource;

        public List<int> DataSource_List;

        [Params(100, 1000)]
        public int Count { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            DataSource = Generator.EnumerableRange(1, Count);
            DataSource_List = DataSource.ToList();
        }

        [Benchmark(Baseline = true, Description = "IEnumerable.Any()")]
        public bool EnumerableAny() => DataSource.Any();

        [Benchmark(Description = "IEnumerable.Count() > 0")]
        public bool EnumerableCountCompareWithZero() => DataSource.Count() > 0;

        [Benchmark(Description = "List.Any()")]
        public bool ListAny() => DataSource_List.Any();

        [Benchmark(Description = "List.Count() > 0")]
        public bool ListCountCompareWithZero() => DataSource_List.Count() > 0;

        [Benchmark(Description = "List.Count > 0")]
        public bool ListCountPropCompareWithZero() => DataSource_List.Count > 0;
    }
}
```

[`IEnumerable<int> Generator.EnumerableRange(int start, int count)`](/src/Common/Utils/Generator.cs#L8)
