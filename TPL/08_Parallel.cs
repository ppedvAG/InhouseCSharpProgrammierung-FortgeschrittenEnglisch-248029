using System.Diagnostics;

namespace TPL;

public class _08_Parallel
{
	static void Main(string[] args)
	{
		int[] iterations = [1000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000];
		foreach (int d in iterations)
		{
			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
			Console.WriteLine($"For Interations: {d}, {sw.ElapsedMilliseconds}ms");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Interations: {d}, {sw2.ElapsedMilliseconds}ms");

			Console.WriteLine("------------------------------------------------------------");
		}

		/*
			For Interations: 1000, 0ms
			ParallelFor Interations: 1000, 40ms
			------------------------------------------------------------
			For Interations: 10000, 1ms
			ParallelFor Interations: 10000, 2ms
			------------------------------------------------------------
			For Interations: 50000, 2ms
			ParallelFor Interations: 50000, 2ms
			------------------------------------------------------------
			For Interations: 100000, 4ms
			ParallelFor Interations: 100000, 26ms
			------------------------------------------------------------
			For Interations: 250000, 10ms
			ParallelFor Interations: 250000, 4ms
			------------------------------------------------------------
			For Interations: 500000, 20ms
			ParallelFor Interations: 500000, 7ms
			------------------------------------------------------------
			For Interations: 1000000, 42ms
			ParallelFor Interations: 1000000, 42ms
			------------------------------------------------------------
			For Interations: 5000000, 230ms
			ParallelFor Interations: 5000000, 94ms
			------------------------------------------------------------
			For Interations: 10000000, 436ms
			ParallelFor Interations: 10000000, 98ms
			------------------------------------------------------------
			For Interations: 100000000, 4564ms
			ParallelFor Interations: 100000000, 1727ms
			------------------------------------------------------------
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i =>
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}