namespace TPL;

public class _03_WaitingForTasks
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		t.Wait(); //Code stops right here
		//Instead of t.Wait() you should always use await

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		Task t2 = null;
		Task t3 = null;
		Task t4 = null;

		Task.WaitAll(t2, t3, t4); //WaitAll: Waits for multiple Tasks
		Task.WaitAny(t2, t3, t4); //WaitAny: Waits on the fastest Task
	}

	static void Run()
	{
		Console.WriteLine("Task started");
		Thread.Sleep(3000);
		Console.WriteLine("Task completed");
	}
}