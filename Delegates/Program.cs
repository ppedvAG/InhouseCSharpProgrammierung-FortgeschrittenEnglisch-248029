namespace Delegates;

internal class Program
{
	/// <summary>
	/// Delegates
	/// Custom type, with a structure like a method head
	/// Has a return value, a name and parameters
	/// 
	/// Is defined using the delegate keyword
	/// Objects of this type, can hold method/function pointers
	/// Every attached method must have the same structure as the delegate
	/// </summary>
	static void Main(string[] args)
	{
		Introduction i = new Introduction(IntroductionEN); //Creation of the delegate with an initial method
														   //The variable i now contains a method pointer to IntroductionEN

		i("Max"); //Execution of the delegate

		//With += and -= methods can be attached/detached
		i += new Introduction(IntroductionEN); //Attaches the EN method again
		i += IntroductionEN; //Same as above, but shorter

		i("John"); //Three outputs, every method will be executed

		i += IntroductionDE;
		i("Max");

		i -= IntroductionEN;
		i -= IntroductionEN;
		i -= IntroductionEN;
		i("John");

		i -= IntroductionDE; //If the last method is removed, the delegate will be null

		if (i is not null)
			i("Max");

		//Null-propagation: Only executes the method after the question mark, if the variable before it, is not null
		i?.Invoke("Max");

		List<int> ints = null;
		ints?.Add(1);

		//Get all methods that are attached to the delegate
		foreach (Delegate dg in i.GetInvocationList())
		{
			Console.WriteLine(dg.Method.Name);
		}
	}

	/// <summary>
	/// Definition of the delegate
	/// </summary>
	public delegate void Introduction(string name);

	static void IntroductionEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}

	static void IntroductionDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}
}