using DelegateComponent;
using System.Diagnostics;

namespace Delegates;

/// <summary>
/// User side
/// </summary>
public class User
{
	static void Main(string[] args)
	{
		ProcessComponent comp = new ProcessComponent();
		comp.Start += Comp_Start;
		comp.Progress += Comp_Progress;
		comp.End += Comp_End;
		comp.Run();
	}

	private static void Comp_Start(object? sender, EventArgs e)
	{
		Console.WriteLine("Process started");
		Debug.Write("Process started"); //Log entry
	}

	private static void Comp_Progress(object? sender, ProgressEventArgs e)
	{
		Console.WriteLine($"Progress: {e.Current}/{e.Maximum}");
		Debug.Write($"Progress: {e.Current}/{e.Maximum}"); //Log entry
	}

	private static void Comp_End(object? sender, EventArgs e)
	{
		Console.WriteLine("Process complete");
		Debug.Write("Process complete"); //Log entry
	}
}