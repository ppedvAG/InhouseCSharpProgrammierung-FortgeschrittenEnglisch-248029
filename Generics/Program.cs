using System.Collections;

namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		//Generics: Placeholder for a concrete Type
		//Usually referred to as T
		//Inside of the method/class the generic type will ALWAYS be T (never a concrete type)
		//Outside of the method/class the generic type will NEVER be T (always a concrete type)

		//Example: List
		List<int> ints = new List<int>(); //T will be replaced with int
		ints.Add(10); //T is replaced by int

		List<string> strings = new List<string>();
		strings.Add("ABC");

		Dictionary<string, int> dict = new Dictionary<string, int>();
		dict.Add("Hello", 1);
	}

	static T Test<T>(T param)
	{
		T obj = default;
		return obj;
	}
}

/// <summary>
/// With the generic type parameter (T), we can now define fields/properties/methods/... with generics inside of the class
/// </summary>
public class DataStore<T> : IEnumerable<T> //T can also be supplemented at inheritance
{
	private T[] _data;

	public List<T> Data => _data.ToList(); //The concrete of List can be supplemented with a generic

	public T Get(int index)
	{
		return _data[index];
	}

	public void Add(T obj, int index)
	{
		_data[index] = obj;
	}

	public IEnumerator<T> GetEnumerator()
	{
		foreach (T obj in _data)
			yield return obj;
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public void Keywords()
	{
		//Three keywords: default, typeof, nameof

		Console.WriteLine(default(T)); //Returns the default value of the type (0, null, false)
		Console.WriteLine(typeof(T)); //Gives the type behind the generic (e.g. when comparing for a specific type)
		Console.WriteLine(nameof(T)); //Gives the name of the generic as a string ("int", "string", "bool", ...)
	}
}