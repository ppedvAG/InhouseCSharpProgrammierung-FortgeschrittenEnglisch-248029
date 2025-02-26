using PluginBase;

namespace PluginCalculator;

public class Calculator : IPlugin
{
	public string Name => "Calculator";

	public string Description => "A simple calculator";

	public string Version => "1.0";

	public string Author => "Lukas";

	[ReflectionVisible]
	public double Add(int x, int y) => x + y;
}
