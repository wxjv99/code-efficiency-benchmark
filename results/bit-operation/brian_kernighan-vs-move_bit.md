``` ini

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3323)
AMD Ryzen 7 7840H with Radeon 780M Graphics, 1 CPU, 16 logical and 8 physical cores

```

``` ini
[Host] : .NET 9.0.2 (9.0.225.6610), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=.NET 9.0   
```
| Method                     | Count | Mean         | StdDev     | Ratio | 
|--------------------------- |------ |-------------:|-----------:|------:|
| **&#39;Brian Kernighan&#39;**          | **100**   |    **549.21 ns** |   **4.822 ns** |  **1.00** | 
| &#39;Move bit(Loop)&#39;           | 100   |    973.80 ns |  10.539 ns |  1.77 | 
| &#39;Move bit(Loop unrolling)&#39; | 100   |     36.96 ns |   0.132 ns |  0.07 | 
|                            |       |              |            |       | 
| **&#39;Brian Kernighan&#39;**          | **1000**  |  **5,727.74 ns** |  **46.998 ns** |  **1.00** | 
| &#39;Move bit(Loop)&#39;           | 1000  | 10,211.05 ns | 305.043 ns |  1.78 | 
| &#39;Move bit(Loop unrolling)&#39; | 1000  |    300.86 ns |   1.385 ns |  0.05 | 


``` csharp
using BenchmarkDotNet.Attributes;
using System;

namespace CodeEfficiencyBenchmark.Benchmarks
{
    public class CountSetBits
    {
        public int[] DataSource;

        [Params(100, 1000)]
        public int Count { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            DataSource = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                DataSource[i] = Random.Shared.Next(int.MaxValue);
            }
        }

        [Benchmark(Baseline = true, Description = "Brian Kernighan")]
        public void CountSetBits1()
        {
            foreach (var item in DataSource)
            {
                var number = item;
                var count = 0;
                while (number != 0)
                {
                    number &= number - 1;
                    count++;
                }
            }
        }

        [Benchmark(Description = "Move bit(Loop)")]
        public void CountSetBits2()
        {
            foreach (var item in DataSource)
            {
                var number = item;
                var count = 0;

                while (number != 0)
                {
                    count += number & 1;
                    number >>= 1;
                }
            }
        }

        [Benchmark(Description = "Move bit(Loop unrolling)")]
        public void CountSetBits3()
        {
            foreach (var item in DataSource)
            {
                var number = item;
                var count = (number & 1) +
                    ((number >> 1) & 1) +
                    ((number >> 2) & 1) +
                    ((number >> 3) & 1) +
                    ((number >> 4) & 1) +
                    ((number >> 5) & 1) +
                    ((number >> 6) & 1) +
                    ((number >> 7) & 1) +
                    ((number >> 8) & 1) +
                    ((number >> 9) & 1) +
                    ((number >> 10) & 1) +
                    ((number >> 11) & 1) +
                    ((number >> 12) & 1) +
                    ((number >> 13) & 1) +
                    ((number >> 14) & 1) +
                    ((number >> 15) & 1) +
                    ((number >> 16) & 1) +
                    ((number >> 17) & 1) +
                    ((number >> 18) & 1) +
                    ((number >> 19) & 1) +
                    ((number >> 20) & 1) +
                    ((number >> 21) & 1) +
                    ((number >> 22) & 1) +
                    ((number >> 23) & 1) +
                    ((number >> 24) & 1) +
                    ((number >> 25) & 1) +
                    ((number >> 26) & 1) +
                    ((number >> 27) & 1) +
                    ((number >> 28) & 1) +
                    ((number >> 29) & 1) +
                    ((number >> 30) & 1) +
                    ((number >> 31) & 1);
            }
        }
    }
}

```
