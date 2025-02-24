namespace TPL;

class _02_TaskWithReturn
{
	static void Main(string[] args)
	{
		//Task: Print the result of the task, as soon as the task itself is done
		Task<int> t = new Task<int>(Calculate);
		t.Start();

		//Console.WriteLine(t.Result); //This blocks the code afterwards
		//Solutions: ContinueWith, await

		bool isPrinted = false;
		for (int i = 0; i < 100; i++)
		{
			if (t.IsCompleted && !isPrinted)
			{
				Console.WriteLine(t.Result);
				isPrinted = true;
			}

			Thread.Sleep(500); //Longer task; we want to print the result of the calculate task as soon as it is completes

			Console.WriteLine($"Main Thread: {i}");
		}

		Console.WriteLine(t.Result);
	}

	static int Calculate()
	{
		Thread.Sleep(2000);
		return Random.Shared.Next();
	}
}