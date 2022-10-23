using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using System.Linq;

namespace CodeEfficiencyBenchmark.Configs
{
    public class Config : ManualConfig
    {
        public static readonly IConfig FxInstance = new Config(ClrRuntime.Net461);

        public static readonly IConfig CoreInstance = new Config(CoreRuntime.Core60);

        public Config(Runtime runtime)
        {
            Options = DefaultConfig.Instance.Options;
            SummaryStyle = DefaultConfig.Instance.SummaryStyle;
            ArtifactsPath = DefaultConfig.Instance.ArtifactsPath;

            AddColumnProvider(DefaultConfig.Instance.GetColumnProviders().ToArray());
            AddExporter(HtmlExporter.Default, MarkdownExporter.GitHub);
            AddLogger(DefaultConfig.Instance.GetLoggers().ToArray());
            AddAnalyser(DefaultConfig.Instance.GetAnalysers().ToArray());
            AddJob(Job.Default
                .WithPowerPlan(PowerPlan.UserPowerPlan)
                .WithRuntime(runtime)
                .WithId(runtime.MsBuildMoniker)
            );
            AddValidator(DefaultConfig.Instance.GetValidators().ToArray());
            HideColumns(StatisticColumn.Error, StatisticColumn.Median, BaselineRatioColumn.RatioStdDev);
        }
    }
}
