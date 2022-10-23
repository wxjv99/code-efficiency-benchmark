using BenchmarkDotNet.Running;
using CodeEfficiencyBenchmark.Configs;

BenchmarkSwitcher.FromAssemblies(new[]
{
    typeof(Config).Assembly,
    typeof(Program).Assembly
}).Run(args, Config.CoreInstance);
