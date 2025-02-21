namespace DelegateComponent;

/// <summary>
/// Developer side
/// </summary>
public class ProcessComponent
{
	public event EventHandler Start;

	public event EventHandler End;

	public event EventHandler<ProgressEventArgs> Progress;

	public void Run()
	{
		Start?.Invoke(this, EventArgs.Empty);

		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200); //Long process (ex. loading data from a DB/API/...)
			Progress?.Invoke(this, new ProgressEventArgs(i + 1, 10));
		}

		End?.Invoke(this, EventArgs.Empty);
	}
}

public class ProgressEventArgs : EventArgs
{
	public int Current { get; }

	public int Maximum { get; }

	public ProgressEventArgs(int current, int maximum)
	{
		Current = current;
		Maximum = maximum;
	}
}