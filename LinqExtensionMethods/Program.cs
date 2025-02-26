using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Serialization;

namespace LinqExtensionMethods;

internal class Program
{
	static void Main(string[] args)
	{
		#region List theory
		//IEnumerable
		//Interface, which gets inherited to every list class in C#
		//e.g. List, Array, Dictionary, Queue, Stack, ObservableCollection, DbSet, ...
		//-> Every list type in C# can use Linq

		//Properties
		//1. If a class has this interface, you can use a foreach loop on the class itself
		//2. If you get an object of the IEnumerable type, it may not contain any data
		//-> IEnumerable is just an instruction manual

		IEnumerable<int> billion = Enumerable.Range(0, 1_000_000_000); //Just an instruction manual -> takes no time/resources

		//List<int> ints = billion.ToList(); //Here actual data is created

		//- if you can, you should always use IEnumerable
		//- you should always execute the manual as late as possible

		int[] test1 = [1, 2, 3];
		List<int> test2 = [4, 5, 6];
		ObservableCollection<int> test3 = [7, 8, 9];

		Test(test1);
		Test(test2);
		Test(test3);

		//Problem: IEnumerable doesn't tell you, if it contains data or not (as a variable type)
		IEnumerable<int> t1 = new int[] { 1, 2, 3 };
		IEnumerable<int> t2 = new List<int> { 1, 2, 3 };
		IEnumerable<int> t3 = new ObservableCollection<int> { 1, 2, 3 };

		//Example
		test2.AddRange(test1); //AddRange consumes IEnumerable, therefore it can consume any list

		//IEnumerator
		//Basic component of all list types
		//Mechanism for enumerating the data

		//Three components
		//- Current: Always gives the element under the pointer
		//- MoveNext(): Moves the pointer to the next element
		//- Reset(): Resets the pointer to the first element

		//A foreach loop always uses the underlying enumerator to run through the data

		List<int> x = Enumerable.Range(0, 20).ToList();
		foreach (int i in x)
		{
			Console.WriteLine(i);
		}

		//Using the enumerator directly
		IEnumerator<int> enumerator = x.GetEnumerator();
		enumerator.MoveNext();
	start:
		Console.WriteLine(enumerator.Current);
		bool moreElements = enumerator.MoveNext();
		if (moreElements)
			goto start;
		enumerator.Reset();
		#endregion

		#region Basic Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

		Console.WriteLine(ints.First()); //First element of the list, throws an exception, if there are no elements
		Console.WriteLine(ints.FirstOrDefault()); //First element of the list, gives the default value, if there are no elements

		Console.WriteLine(ints.Last()); //Last element of the list, throws an exception, if there are no elements
		Console.WriteLine(ints.LastOrDefault()); //Last element of the list, gives the default value, if there are no elements

		//Console.WriteLine(ints.Single());
		//Console.WriteLine(ints.SingleOrDefault());

		//Console.WriteLine(ints.First(e => e % 50 == 0));
		Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0));
		#endregion

		#region Linq with objects
		List<Car> cars = new List<Car>
		{
			new Car(251, Brand.BMW),
			new Car(274, Brand.BMW),
			new Car(146, Brand.BMW),
			new Car(208, Brand.Audi),
			new Car(189, Brand.Audi),
			new Car(133, Brand.VW),
			new Car(253, Brand.VW),
			new Car(304, Brand.BMW),
			new Car(151, Brand.VW),
			new Car(250, Brand.VW),
			new Car(217, Brand.Audi),
			new Car(125, Brand.Audi)
		};

		//Find all cars, with MaxV greater than 250
		cars.Where(e => e.MaxV > 250); //Every Linq function that returns a list, always returns IEnumerable

		//Find all BMWs, with MaxV greater than 250
		cars.Where(e => e.CarBrand == Brand.BMW && e.MaxV > 250);
		cars.Where(e => e.CarBrand == Brand.BMW).Where(e => e.MaxV > 250); //You can chain multiple Linq statements together

		//Sort all cars by brand
		cars.OrderBy(e => e.CarBrand);

		//Sort all cars by brand, then by MaxV
		cars.OrderBy(e => e.CarBrand).ThenBy(e => e.MaxV);
		cars.OrderByDescending(e => e.CarBrand).ThenByDescending(e => e.MaxV);

		//Do all cars drive at least 200kph?
		if (cars.All(e => e.MaxV > 200))
		{
			//...
		}

		//Does at least one car drive at least 200kph?
		if (cars.Any(e => e.MaxV > 200))
		{
			//...
		}

		//Use cases all/any
		//Check if a string only contains letters
		string text = "Hello";
		text.All(char.IsLetter);

		//Check if a list contains elements
		cars.Any();

		//How many BMWs do we have?
		cars.Count(e => e.CarBrand == Brand.BMW); //4 (int)
		cars.Where(e => e.CarBrand == Brand.BMW); //4 elements
		cars.Where(e => e.CarBrand == Brand.BMW).Count(); //4 (int)

		//MinBy, MaxBy, Sum, Average
		//Returns the object, with the lowest/highest value for a criteria
		cars.MinBy(e => e.MaxV); //A car object
		cars.Min(e => e.MaxV); //A number (int)

		cars.Average(e => e.MaxV); //208.41666666666666
		cars.Sum(e => e.MaxV);

		//Skip/Take
		//Webshop
		int pageNumber = 1;
		cars.Skip(pageNumber * 10).Take(10);

		cars.Chunk(10); //Divides the list into arrays with X size each

		//Top 3
		//The top 3 fastest cars
		cars.OrderByDescending(e => e.MaxV)
			.Take(3);

		//Select
		//Transforms a list
		//Takes a lambda expression and puts every element into this function
		//Then, it takes the return values of the function, and creates a new list with them

		//1. Extract a single field from a list (80%)

		//Create a list that only contains the car brands
		IEnumerable<Brand> brands = cars.Select(e => e.CarBrand);
		brands.Distinct(); //Show each brand only once

		//2. Transformation of the list (20%)

		//Read all files from a directory, and only get the filenames (without extension and path)
		string[] paths = Directory.GetFiles("C:\\Windows");
		List<string> fileNames = [];
		foreach (string p in paths)
			fileNames.Add(Path.GetFileNameWithoutExtension(p));

		List<string> fn = Directory.GetFiles("C:\\Windows")
			.Select(Path.GetFileNameWithoutExtension) //returns a list, which contains all elements transformed by the function within the Select itself
			.ToList()!;

		Console.WriteLine(fileNames.SequenceEqual(fn));

		//Cast an entire list to another type
		List<int> numbers = Enumerable.Range(0, 20).ToList();

		//Convert this int List to a double List
		//numbers.Cast<double>().ToList(); //doesn't work
		numbers.Select(e => (double) e);

		//List with 0.1 steps
		Enumerable.Range(0, 100).Select(e => e / 10.0);

		//SelectMany
		//Flattens a list by a criteria
		List<int[]> y = 
		[
			[1, 2, 3],
			[4, 5, 6],
			[7, 8, 9]
		];

		y.SelectMany(e => e);

		//GroupBy
		//Creates a grouping by a criteria
		//-> Creates sublists, where each element of the list will be put into a category
		cars.GroupBy(e => e.CarBrand);

		Dictionary<Brand, List<Car>> dict = cars
			.GroupBy(e => e.CarBrand)
			.ToDictionary(e => e.Key, e => e.ToList());
		Console.WriteLine(dict[Brand.BMW].Count);
		#endregion

		#region	Extension Methods
		int n = 21849;
		Console.WriteLine(n.Crosssum()); //Every int in the entire project now has this function

		//After compilation
		Console.WriteLine(ExtensionMethods.Crosssum(n));

		//Linq works the same way
		cars.Where(e => e.CarBrand == Brand.BMW);
		Enumerable.Where(cars, e => e.CarBrand == Brand.BMW);

		//Optimzing XmlSerializer using extension methods

		//1. Create StreamWriter automatically
		XmlSerializer xml = new XmlSerializer(cars.GetType());
		//using (StreamWriter sw = new StreamWriter("Test.xml"))
		//{
		//	xml.Serialize(sw, cars);
		//}
		xml.Serialize("Test.xml", cars);

		//2. Cast the converted Xml string to a type automatically
		//using (StreamReader sr = new StreamReader("Test.xml"))
		//{
		//	List<Car> readCars = (List<Car>) xml.Deserialize(sr);
		//}
		List<Car> readCars = xml.Deserialize<List<Car>>("Test.xml");
		#endregion
	}

	/// <summary>
	/// This function can now take any list type
	/// </summary>
	static void Test(IEnumerable<int> x) { }
}

public record Car(int MaxV, Brand CarBrand);

public enum Brand { Audi, BMW, VW }

public static class XmlExtensions
{
	public static void Serialize(this XmlSerializer xml, string path, object o)
	{
		using StreamWriter sw = new StreamWriter(path);
		xml.Serialize(sw, o);
	}

	public static T Deserialize<T>(this XmlSerializer xml, string path)
	{
		using StreamReader sr = new StreamReader(path);
		return (T) xml.Deserialize(sr);
	}
}