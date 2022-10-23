using BenchmarkDotNet.Running;
using CodeEfficiencyBenchmark.Configs;

namespace CodeEfficiencyBenchmark.NetFx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssemblies(new[]
            {
                typeof(Config).Assembly,
                typeof(Program).Assembly
            }).Run(args, Config.FxInstance);
        }
    }
}
