using AlexandruMaries.Data.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Benchmarks.Repositories;

public class BaseRepositoryBenchmark
{
    protected ServiceProvider _serviceProvider;

    public virtual void GlobalSetup()
    {
        var connectionString =
            ""; // complete with ConnectionString

        _serviceProvider = new ServiceCollection()
            .AddDataServices(connectionString)
            .BuildServiceProvider();
    }
}