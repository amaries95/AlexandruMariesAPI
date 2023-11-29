using AlexandruMaries.Data.Interfaces;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Benchmarks.Repositories;

[MemoryDiagnoser]
[IterationCount(10)]
[WarmupCount(10)]
public class ReferenceRepositoryBenchmark : BaseRepositoryBenchmark
{
    private IReferenceRepository _referenceRepository;

    [GlobalSetup]
    public override void GlobalSetup()
    {
        base.GlobalSetup();

        _referenceRepository = _serviceProvider.GetRequiredService<IReferenceRepository>();
    }

    //| Method                | Mean     | Error     | StdDev    | Allocated |
    //|---------------------- |---------:|----------:|----------:|----------:|
    //| GetAllReferences      | 60.65 ms | 16.877 ms | 11.163 ms |  19.06 KB |
    //| GetAllVisibleReferences | 39.63 ms |  0.429 ms |  0.256 ms |  14.23 KB |

    [Benchmark]
    public async Task GetAllReferences()
    {
        await _referenceRepository.GetAllReferences(false);
    }

    [Benchmark]
    public async Task GetAllReferenceDapper()
    {
        await _referenceRepository.GetAllVisibleReferences();
    }
}