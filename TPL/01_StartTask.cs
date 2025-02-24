namespace TPL;

internal class _01_StartTask
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //Creates a Task to run the given Action (method pointer)
								//Don't forget to start your task
		t.Start();

		//Task with parameter
		//Requires an Action with an object parameter
		Task t2 = new Task(Run, 500);
		t2.Start();

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		//Foreground threads & background threads
		//When all foreground threads are finished, all background threads get terminated
		//Every Task itself is a background thread
		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Side Task: {i}");
		}
	}

	static void Run(object o)
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
			{
				Console.WriteLine($"Side Task: {i}");
			}
		}
	}
}
