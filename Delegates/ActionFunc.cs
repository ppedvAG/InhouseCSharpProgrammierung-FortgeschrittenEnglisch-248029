namespace Delegates;

public class ActionFunc
{
	static void Main(string[] args)
	{
		//Action
		//Method pointer with void as the return value and up to 16 parameters
		
		//Action without parameters
		Action a = new Action(Output); //Requires a method with void as the return value and 0 parameters
		
		//Execute the delegate
		a();
		a?.Invoke();

		//Attach/detach methods
		a += new Action(Output);
		a -= Output;

		//Iterate the action
		foreach (Delegate dg in a.GetInvocationList())
		{
			Console.WriteLine(dg.Method.Name);
		}

		//Action with parameters
		//If the action shall have parameters, they need to be defined via the generic type arguments
		Action<int, int> b = new Action<int, int>(Add);
		b?.Invoke(4, 8);

		//Example
		//List.ForEach
		List<int> ints = [1, 2, 3, 4, 5, 6, 7, 8, 9];

		//Method 1
		Action<int> methodPointer = new Action<int>(PrintElement);
		ints.ForEach(methodPointer);

		Console.WriteLine("---------------------");

		//Method 2 (equal to method 1)
		ints.ForEach(PrintElement);

		//The code behind the ForEach function
		//With the Delegate Logic, we can add our own code to C# internal functions
		foreach (int i in ints)
		{
			PrintElement(i);
		}

		///////////////////////////////////////////////////////////////////////////////

		//Func
		//Method pointer with any type as the return value and up to 16 parameters
		//The return type is always the last type of the Func
		Func<int> f = new Func<int>(CurrentDay); //The function here can only be of the int return type and cannot have any parameters
		int x = f(); //If we execute a func, it always gives a result
		int? y = f?.Invoke(); //If we want to use ?.Invoke, we have to convert the resulting type into a nullable type (int?)

		//Func with parameters
		Func<int, int, double> g = Divide;
		double result = g.Invoke(7, 3);

		//With null check
		double? result2 = g?.Invoke(7, 3);

		if (g is not null)
		{
			double result3 = g.Invoke(7, 3);
		}

		double result4 = g?.Invoke(7, 3) ?? double.NaN; //if g is null, the result will be 0, otherwise it will calculate the result

		//Example
		//List.Where
		//Where filters the list by a predicate

		//Exercise: Find all numbers, that are divisble by 2
		//What is Func<int, bool>? -> A function that returns a bool and takes 1 parameter of type int
		ints.Where(DivisibleBy2);

		List<int> div2 = [];
		foreach (int n in ints)
		{
			if (DivisibleBy2(n))
			{
				div2.Add(n);
			}
		}

		///////////////////////////////////////////////////////////////////////////////

		//Anonymous functions
		//Functions, that are not defined as normal outside of the method, but are instead just saved in a variable and mostly used once

		g += delegate (int x, int y) { return (double) x / y; }; //Anonymous method

		//The delegate keyword could be omitted
		g += (int x, int y) => { return (double) x / y; }; //Shorter form

		//Types of x and y can now be inferred by the delegate itself (Func<int, int, double>)
		g += (x, y) => { return (double) x / y; };

		//The return keyword can be omitted, if the brackets are omitted
		g += (x, y) => (double) x / y; //Shortest, most common form

		//ForEach and Where can also be supplied with anonymous functions instead of defined functions
		ints.ForEach(e => Console.WriteLine(e)); //e: Current element
		ints.ForEach(Console.WriteLine); //The Console.WriteLine method pointer just fits in here
		ints.Where(e => e % 2 == 0); //e: Current element
	}

	#region Action
	static void Output()
	{
		Console.WriteLine("Hello this is a test");
	}

	static void Add(int a, int b)
	{
		Console.WriteLine($"{a} + {b} = {a + b}");
	}

	static void PrintElement(int x)
	{
		Console.WriteLine(x);
	}
	#endregion

	#region	Func
	static int CurrentDay()
	{
		return DateTime.Now.Day;
	}

	static double Divide(int x, int y)
	{
		return (double) x / y;
	}

	static bool DivisibleBy2(int x)
	{
		return x % 2 == 0;
	}
	#endregion
}