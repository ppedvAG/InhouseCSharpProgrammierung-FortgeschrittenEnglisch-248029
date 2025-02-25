namespace AsyncAwaitWPF;

/// <summary>
/// Throws out data in random intervals
/// At the other side (where the data source is used), we can await it using an await foreach loop
/// 
/// Example: Livestream
/// - Throws images at your PC
/// - Your PC combines them to a video
/// - But the stream could suddenly end/be slowed
/// </summary>
public class AsyncDataSource
{
	public async IAsyncEnumerable<int> GenerateNumbers()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next(500, 2000));

			//yield return: returns a value, but doesn't end the function
			//Can only be used, if the return type of a function is an IEnumerable or a derivative
			yield return Random.Shared.Next();
		}
	}
}