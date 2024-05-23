
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;

namespace FindPrimes
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        const int MAX = 2_147_483_591; // int.MaxValue - 56;

        [Params(10, 100, 1_000)]
        //for those that are fast and memory effecent
        //[Params(1_000_000, 100_000_000, MAX)]
        public int N { get; set; }



        [Benchmark]
        public int Definition()
        {
            var uut = new Definition(N);
            uut.Initialize();
            return uut.CountPrimes();
        }

        public static IConfig CreateTestConfig()
        {
            var cfg = ManualConfig.CreateMinimumViable()
                .AddJob(Job.Dry)
                .AddValidator(ReturnValueValidator.FailOnError)
               ;
            return cfg;
        }

        public  static IConfig CreateConfig()
        {
            var cfg = ManualConfig.CreateMinimumViable()
                .AddJob(Job.MediumRun)
                .AddColumn(new RankColumn(NumeralSystem.Arabic))
                .AddExporter(HtmlExporter.Default)
                .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest))
                //.AddValidator(ReturnValueValidator.FailOnError)
                //custom Exporter
                .AddExporter(new CsvExporter(
                    CsvSeparator.CurrentCulture,
                    new SummaryStyle(
                        cultureInfo: System.Globalization.CultureInfo.CurrentCulture,
                        printUnitsInHeader: true,
                        printUnitsInContent: false,
                        timeUnit: Perfolizer.Horology.TimeUnit.Nanosecond,
                        sizeUnit: SizeUnit.KB
                    )
                ))
                ;
            return cfg;
        }


    }



}
 