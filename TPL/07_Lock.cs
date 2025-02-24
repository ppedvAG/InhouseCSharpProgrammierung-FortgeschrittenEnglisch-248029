using System.Collections.Generic;

namespace TPL;

class _07_Lock
{
	public static int Counter;

	public static List<int> CounterList = new(100000);

	public static object LockObject { get; set; } = new();

	static void Main(string[] args)
	{
		List<Task> tasks = [];
		for (int i = 0; i < 100; i++)
		{
			tasks.Add(Task.Run(TaskAction));
		}
		Task.WaitAll(tasks);
	}

	public static void TaskAction()
	{
		for (int i = 0; i < 500; i++)
			CounterIncrement();
	}

	public static void CounterIncrement()
	{
		//If the lock is set, the tasks wait here for the lock to be released
		//The next task in line, will execute this code block
		//lock (LockObject)
		//{
		//	Counter++;
		//	Console.WriteLine(Counter);
		//}

		////Provides integer operations and locks automatically
		//Interlocked.Add(ref Counter, 1);

		Interlocked.Add(ref Counter, 1);

		CounterList.Add(Counter);
	}
}
