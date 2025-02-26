using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		Assembly a = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2025_02_19_EN\PluginCalculator\bin\Debug\net9.0\PluginCalculator.dll"); //The path of the DLL (e.g. from a config file)

		Type t = a.GetType("PluginCalculator.Calculator");

		IPlugin plugin = (IPlugin) Activator.CreateInstance(t);

		Console.WriteLine("Loaded plugin: ");
		Console.WriteLine(plugin.Name);
		Console.WriteLine(plugin.Description);
		Console.WriteLine(plugin.Version);
		Console.WriteLine(plugin.Author);

		Console.WriteLine("-----------------------");

		foreach (MethodInfo m in t.GetMethods().Where(e => e.GetCustomAttribute<ReflectionVisible>() != null))
		{
			Console.WriteLine(m.Name);
		}
	}
}
