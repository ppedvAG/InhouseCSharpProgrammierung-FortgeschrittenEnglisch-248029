namespace TopLevelStatements;

public class Scanner
{
	private string scanPath;

	private Task scanTask;

	public Scanner(string scanPath)
	{
		this.scanPath = scanPath;
		scanTask = new Task(Run);
		scanTask.Start();
	}

	public void Run()
	{
		while (true)
		{
			string[] imagePaths = Directory.GetFiles(scanPath);

			foreach (string p in imagePaths)
				if (!Program.ImagePaths.Contains(p))
					Program.ImagePaths.Enqueue(p);

			Thread.Sleep(1000);
		}
	}
}