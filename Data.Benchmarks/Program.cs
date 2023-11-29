using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
//    .Run(args, new DebugInProcessConfig()); // for debug

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
    .Run(args);