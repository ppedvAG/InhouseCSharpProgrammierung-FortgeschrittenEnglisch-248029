namespace TPL;

class _05_ExceptionsInTasks
{
	static void Main(string[] args)
	{
		Task t = new Task(Exception1);
		t.Start();

		//Three methods: t.Wait(), Task.WaitAll(...), t.Result

		try
		{
			t.Wait();
		}
		catch (AggregateException ex)
		{
			foreach (Exception e in ex.InnerExceptions)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}

		Console.ReadKey();
	}

	static void Exception1()
	{
		Thread.Sleep(2000);
		throw new AccessViolationException("Test");
	}
}