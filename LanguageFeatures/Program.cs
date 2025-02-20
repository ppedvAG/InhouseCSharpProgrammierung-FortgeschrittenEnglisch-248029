using IntList = System.Collections.Generic.List<int>;

namespace LanguageFeatures;

internal unsafe class Program
{
	unsafe static void Main(string[] args)
	{
		int x = 219_4_0_1_2_0_4;

		//class and struct

		//class
		//Reference type
		//If we assign an object to a variable, the variable will contain a pointer to the object
		//If we compare two variables, the HashCodes (Memory addresses) will be compared
		//Person p = new Person(); //The object will be created in memory, and p contains a pointer
		//Person p2 = p; //Here we create another pointer to the object below p
		//p.ID = 10; //If p is changed, p2 is also changed

		//Console.WriteLine(p == p2);
		//Console.WriteLine(p.GetHashCode() == p2.GetHashCode());

		//struct
		//Value type
		//If we assign an object to a variable, the variable will contain a copy of the original object
		//If we compare two variables, the contents will be compared
		int a = 5;
		int b = a; //Here the value from a will be copied to b
		a = 10; //b is unchanged

		Console.WriteLine(a == b);

		//Reference structs
		int c = 5;
		ref int d = ref c; //Here we create a reference to c
		c = 10; //Both values are changed

		Test(z: 10);

		unsafe
		{

		}

		using StreamWriter sw = new StreamWriter("Test.txt");

		//Null-Coalescing Operator (??-Operator): Take the left side, if it is not null, otherwise take the right side

		string str = "Hello World";

		//No Operator
		if (str != null)
			Console.WriteLine(str);
		else
			Console.WriteLine("str is empty");

		//?-Operator
		Console.WriteLine(str != null ? str : "str is empty");

		//??-Operator
		Console.WriteLine(str ?? "str is empty"); //If str is not null, write str, else "str is empty"

		List<int> ints = null;
		if (ints == null)
			ints = new List<int>();

		ints = ints == null ? new List<int>() : ints;

		ints ??= new List<int>();

		//String Interpolation ($-String): Implement Code inside of a string
		int i = 10;
		bool t = true;
		string s = "Hello";

		//Combine i, t, s
		Console.WriteLine("i is: " + i + ", t is: " + t + ", s is: " + s);

		Console.WriteLine($"i is: {i}, t is: {t}, s is: {s}"); //With Brackets { } you can implement code inside of the string

		//Verbatim-String (@-String): Ignoring Escape-Sequences
		string path = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\9.0.2\System.Security.Claims.dll";

		DayOfWeek day = DateTime.Now.DayOfWeek;
		switch (day)
		{
			//case DayOfWeek.Monday:
			//case DayOfWeek.Tuesday:
			//case DayOfWeek.Wednesday:
			//case DayOfWeek.Thursday:
			//case DayOfWeek.Friday:
			//	break;
			case >= DayOfWeek.Monday and <= DayOfWeek.Friday:
				break;
			case DayOfWeek.Saturday or DayOfWeek.Sunday:
				break;
		}

		Person r = new(0, "John", DateTime.Now);
		var (id, name, bd) = r; //Deconstruct: Splits the object into a tuple
		Console.WriteLine(id);
		Console.WriteLine(name);
		Console.WriteLine(bd);

		////////////////////////////////////////////////////////////////////////////
		
		void Test2(params int[] x)
		{

		}

		Test2(1, 2, 3);
		Test2(1, 2, 3, 4, 5);
		Test2(1);
		Test2();

		//ArrayTest at = new ArrayTest()
		//{
		//	Ints =
		//	{
		//		[^0] = 1,
		//		[1] = 1,
		//		[2] = 1,
		//		[3] = 1
		//	}
		//};

		Console.WriteLine(DateTime.Now > DateTime.MinValue);

		double m = 234;
		int k = (int) m; //explicit conversion

		int n = 138;
		double g = n; //implicit conversion


		int[] test = new int[10]; //10 ints * 4 Byte = 40 Byte in Memory
		string s1 = "Hello"; //Hello in Memory
		string s2 = "World"; //World in Memory
		s1 += s2; //Hello, World, HelloWorld in Memory


		string complete = "";
		for (int z = 0; z < 100; z++)
		{
			complete += z.ToString();
			Console.WriteLine(complete);
		}

		Console.WriteLine("------------------------------------");

		StringBuilder completeBuilder = new();
		for (int z = 0; z < 100; z++)
		{
			completeBuilder.Append(z);
		}
		Console.WriteLine(completeBuilder.ToString());
	}

	static void Test(ref int x) => Console.WriteLine(x);

	static void Test(int x = 0, int y = 0, int z = 0) { }

	static string Today()
	{
		switch (DateTime.Now.DayOfWeek) //Control + Dot: Quick Options
		{
			case DayOfWeek.Monday:
				return "Monday";
			case DayOfWeek.Tuesday:
				return "Tuesday";
			default:
				return "Other day";
		}
	}
}

//public class Person(int ID)
//{
//	public int ID { get; set; }

//	public Person(int ID)
//	{
//		this.ID = ID;
//	}
//}

public record Person(int ID, string Name, DateTime Birthdate);

public interface Test
{
	void TestMethod2();

	public void TestMethod()
	{
		//...
		//Bad Practice
	}
}

public class ArrayTest
{
	public int[] Ints { get; set; }
}