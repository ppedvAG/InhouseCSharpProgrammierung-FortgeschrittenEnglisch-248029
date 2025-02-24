namespace TPL;

class _06_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith: If a task is complete, start another task as a follow-up
		//In the follow-up task, we have access to the previous task

		//Exercise: Print the calculation result, as soon as it is complete
		Task<int> t = new Task<int>(Calculate);

		//Every single overload always has access to the previous task
		t.ContinueWith(previousTask => Console.WriteLine(previousTask.Result), TaskContinuationOptions.OnlyOnRanToCompletion);

		//TaskContinuationOptions: Conditional continuations (start this follow-up task only, if a condition is met)
		t.ContinueWith(prevTask => Console.WriteLine(prevTask.Exception.Message), TaskContinuationOptions.OnlyOnFaulted);
		t.Start();

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(5000);
		}

		Console.ReadKey();
	}

	static int Calculate()
	{
		Thread.Sleep(2000);

		if (Random.Shared.Next() % 2 == 0)
			throw new Exception("50% hit");

		return Random.Shared.Next();
	}
}
