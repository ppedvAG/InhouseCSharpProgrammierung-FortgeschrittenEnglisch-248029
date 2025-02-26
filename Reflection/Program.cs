using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		//Reflection
		//Determination of information about objects during runtime

		//Everything in Reflection starts at the Type object

		//Two options to obtain a type

		//1. GetType(), gives the type behind a variable (the attached object)
		object x = "123";
		Type xt = x.GetType();

		//2. typeof(...), gives the type object via a type name (class, struct, enum, delegate, ...)
		Type pt = typeof(Program);

		//Possible information to obtain via a type
		MethodInfo[] methods = pt.GetMethods();
		PropertyInfo[] properties = pt.GetProperties();
		ConstructorInfo[] constructors = pt.GetConstructors();
		FieldInfo[] fields = pt.GetFields();

		////////////////////////////////////////////////////////////////

		//Example 1: Set a property within an object and run a method using reflection
		object o = Activator.CreateInstance(pt);
		pt.GetProperty("Text").SetValue(o, "Hello World");
		pt.GetMethod("Test").Invoke(o, null);

		//Example 2: Load the DelegateComponent from the DLL, and use it

		//Loading a DLL
		Assembly a = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2025_02_19_EN\DelegateComponent\bin\Debug\net9.0\DelegateComponent.dll");

		Type compType = a.GetType("DelegateComponent.ProcessComponent");

		object comp = Activator.CreateInstance(compType);
		compType.GetEvent("Start").AddEventHandler(comp, new EventHandler((object sender, EventArgs args) => Console.WriteLine("Reflection Start")));
		compType.GetEvent("End").AddEventHandler(comp, new EventHandler((object sender, EventArgs args) => Console.WriteLine("Reflection Start")));
		compType.GetEvent("Progress").AddEventHandler(comp, new EventHandler((object sender, EventArgs args)
			=> Console.WriteLine($"Reflection Progress: {args.GetType().GetProperty("Current").GetValue(args)}/{args.GetType().GetProperty("Maximum").GetValue(args)}")));
		compType.GetMethod("Run").Invoke(comp, null);
	}

	public string Text { get; set; }

	public void Test()
	{
		Console.WriteLine(Text);
	}
}
