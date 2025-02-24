namespace TPL;

class _04_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Create a source, that manages any number of tokens
																	 //If you want to cancel something, you always have to go to the source
		CancellationToken ct = cts.Token; //The source creates any number of tokens (structs)

		Task t = new Task(Run, ct);
		t.Start();

		Thread.Sleep(500);
		cts.Cancel(); //Cancels all associated tokens after 500ms

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				//if (ct.IsCancellationRequested)
				//	break;

				ct.ThrowIfCancellationRequested();

				Thread.Sleep(25);
				Console.WriteLine($"Side task: {i}");
			}
		}
	}
}