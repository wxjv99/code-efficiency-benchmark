``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.675)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores

```

``` ini
[Host] : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2

Runtime=.NET 6.0  
```
|                          Method | Depth | Count |        Mean |    StdDev | Ratio |
|-------------------------------- |------ |------ |------------:|----------:|------:|
| **JsonConvert.DeserializeObject()** |     **2** |     **8** |    **69.59 μs** |  **1.292 μs** |  **1.00** |
|    JsonSerializer.Deserialize() |     2 |     8 |    36.64 μs |  0.597 μs |  0.53 |
|                                 |       |       |             |           |       |
| **JsonConvert.DeserializeObject()** |     **4** |     **8** | **4,505.52 μs** | **97.226 μs** |  **1.00** |
|    JsonSerializer.Deserialize() |     4 |     8 | 2,480.98 μs | 43.686 μs |  0.55 |
|                                 |       |       |             |           |       |
| **JsonConvert.SerializeObject()** |     **2** |     **8** |    **32.54 μs** |  **0.556 μs** |  **1.00** |
|    JsonSerializer.Serialize() |     2 |     8 |    15.45 μs |  0.090 μs |  0.48 |
|                               |       |       |             |           |       |
| **JsonConvert.SerializeObject()** |     **4** |     **8** | **2,421.41 μs** | **31.562 μs** |  **1.00** |
|    JsonSerializer.Serialize() |     4 |     8 | 1,189.59 μs | 23.463 μs |  0.49 |


``` ini
[Host] : .NET Framework 4.8.1 (4.8.9075.0), X86 LegacyJIT

Runtime=.NET Framework 4.6.1  
```
|                          Method | Depth | Count |        Mean |    StdDev | Ratio |
|-------------------------------- |------ |------ |------------:|----------:|------:|
| **JsonConvert.DeserializeObject()** |     **2** |     **8** |    **95.60 μs** |  **0.549 μs** |  **1.00** |
|    JsonSerializer.Deserialize() |     2 |     8 |    72.40 μs |  0.830 μs |  0.76 |
|                                 |       |       |             |           |       |
| **JsonConvert.DeserializeObject()** |     **4** |     **8** | **6,273.43 μs** | **58.118 μs** |  **1.00** |
|    JsonSerializer.Deserialize() |     4 |     8 | 4,809.52 μs | 35.340 μs |  0.77 |
|                                 |       |       |             |           |       |
| **JsonConvert.SerializeObject()** |     **2** |     **8** |    **51.80 μs** |  **0.360 μs** |  **1.00** |
|    JsonSerializer.Serialize() |     2 |     8 |    43.24 μs |  0.300 μs |  0.83 |
|                               |       |       |             |           |       |
| **JsonConvert.SerializeObject()** |     **4** |     **8** | **3,634.36 μs** | **11.981 μs** |  **1.00** |
|    JsonSerializer.Serialize() |     4 |     8 | 2,955.99 μs | 20.485 μs |  0.81 |


``` csharp
using BenchmarkDotNet.Attributes;
using CodeEfficiencyBenchmark.Common.Types;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CodeEfficiencyBenchmark.NetCore.Benchmarks.JsonConversion
{
    public class JsonDeserialize
    {
        [Params(2, 4)]
        public int Depth { get; set; }

        [Params(8)]
        public int Count { get; set; }

        public string JString { get; set; }

        public JsonDeserialize()
        {
            JString = "{Name: 'Name_0_0', Value: 0, Chilfren: []}";
        }

        [GlobalSetup]
        public void Setup()
        {
            JString = JsonSerializer.Serialize(JsonObject.Generate(Depth, Count));
        }

        [Benchmark(Baseline = true, Description = "JsonConvert.DeserializeObject()")]
        public void JsonConvertFrom() => JsonConvert.DeserializeObject<JsonObject>(JString);

        [Benchmark(Description = "JsonSerializer.Deserialize()")]
        public void JsonSerializerFrom() => JsonSerializer.Deserialize<JsonObject>(JString);
    }

    public class JsonSerialize
    {
        [Params(2, 4)]
        public int Depth { get; set; }

        [Params(8)]
        public int Count { get; set; }

        public JsonObject JObject { get; set; }

        public JsonSerialize()
        {
            JObject = new JsonObject("Name_0_0", 0, Array.Empty<JsonObject>());
        }

        [GlobalSetup]
        public void Setup()
        {
            JObject = JsonObject.Generate(Depth, Count);
        }

        [Benchmark(Baseline = true, Description = "JsonConvert.SerializeObject()")]
        public void JsonConvertTo() => JsonConvert.SerializeObject(JObject);

        [Benchmark(Description = "JsonSerializer.Serialize()")]
        public void JsonSerializerTo() => JsonSerializer.Serialize(JObject);
    }
}
```

[`JsonObject JsonObject.Generate(int depth, int count)`](/src/Common/Types/JsonObject.cs#L21)
