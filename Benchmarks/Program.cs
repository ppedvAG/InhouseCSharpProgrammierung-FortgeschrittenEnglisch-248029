using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace Benchmarks;

internal class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<StringBenchmarks>();
	}
}

[MemoryDiagnoser(false)]
public class StringBenchmarks
{
	[Params(10000, 100000, 1000000)]
	public int Amount { get; set; }

	[Benchmark]
	[IterationCount(50)]
	public void StringPlusBenchmark()
	{
		string complete = "";
		for (int z = 0; z < Amount; z++)
		{
			complete += z.ToString();
		}
		//Console.WriteLine(complete);
		string end = complete;
	}

	[Benchmark]
	[IterationCount(50)]
	public void StringBuilderBenchmark()
	{
		StringBuilder complete = new();
		for (int z = 0; z < Amount; z++)
		{
			complete.Append(z);
		}
		//Console.WriteLine(complete.ToString());
		string end = complete.ToString();
	}

	[Benchmark]
	[IterationCount(50)]
	public void StringInterpolationBenchmark()
	{
		string complete = "";
		for (int z = 0; z < Amount; z++)
		{
			complete = $"{complete}{z}";
		}
		//Console.WriteLine(complete);
		string end = complete;
	}
}