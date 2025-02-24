using System.Diagnostics;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;



//using System.Text.Json;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Serialization;

internal class Program
{
	static void Main(string[] args)
	{
		List<Car> cars = new List<Car>
		{
			new Car(251, Brand.BMW),
			new RaceCar(274, Brand.BMW),
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

		using (StreamWriter sw = new StreamWriter("Test.csv"))
		{
			CsvWriter writer = new CsvWriter(sw, CultureInfo.CurrentCulture);
			writer.WriteRecords(cars);
		}

		using (StreamReader sr = new StreamReader("Test.csv"))
		{
			CsvReader reader = new CsvReader(sr, CultureInfo.CurrentCulture);
			IEnumerable<Car> readCars = reader.GetRecords<Car>();
		}
	}

	static void SystemJson()
	{
		//List<Car> cars = new List<Car>
		//{
		//	new Car(251, Brand.BMW),
		//	new RaceCar(274, Brand.BMW),
		//	new Car(146, Brand.BMW),
		//	new Car(208, Brand.Audi),
		//	new Car(189, Brand.Audi),
		//	new Car(133, Brand.VW),
		//	new Car(253, Brand.VW),
		//	new Car(304, Brand.BMW),
		//	new Car(151, Brand.VW),
		//	new Car(250, Brand.VW),
		//	new Car(217, Brand.Audi),
		//	new Car(125, Brand.Audi)
		//};

		////System.Text.Json
		//JsonSerializerOptions options = new();
		//options.WriteIndented = true;
		//options.IncludeFields = true;

		////1. Serialization and Deserialization
		//string json = JsonSerializer.Serialize(cars, options);
		//File.WriteAllText("Cars.json", json); //Important: All fields must be properties

		//string readJson = File.ReadAllText("Cars.json");
		//Car[] readCars = JsonSerializer.Deserialize<Car[]>(readJson, options);

		////2. Options/Settings
		////See above

		////3. Attributes
		////- JsonIgnore
		////- JsonExtensionData: Saves every field, that does not have a matching field within the class, into a dictionary
		////- JsonDerivedType: Enabled inheritance within the de-/serialization process (Put the attributes only on the superclass)

		////4. Json by Hand
		//JsonDocument doc = JsonDocument.Parse(readJson); //JsonDocument is a class for holding generic json text
		//foreach (JsonElement element in doc.RootElement.EnumerateArray()) //Each element here is a single car
		//{
		//	int maxV = element.GetProperty("MaxV").GetInt32();
		//	Brand brand = (Brand) element.GetProperty("CarBrand").GetInt32();

		//	Console.WriteLine(maxV);
		//	Console.WriteLine(brand);
		//	Console.WriteLine("----------------");
		//}
	}

	static void NewtonsoftJson()
	{
		List<Car> cars = new List<Car>
		{
			new Car(251, Brand.BMW),
			new RaceCar(274, Brand.BMW),
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

		//System.Text.Json
		JsonSerializerSettings options = new();
		//options.Formatting = Formatting.Indented;
		options.TypeNameHandling = TypeNameHandling.Objects;

		//1. Serialization and Deserialization
		string json = JsonConvert.SerializeObject(cars, options);
		File.WriteAllText("Cars.json", json); //Important: All fields must be properties

		string readJson = File.ReadAllText("Cars.json");
		Car[] readCars = JsonConvert.DeserializeObject<Car[]>(readJson, options);

		//2. Options/Settings
		//See above

		//3. Attributes
		//- JsonIgnore
		//- JsonExtensionData: Saves every field, that does not have a matching field within the class, into a dictionary
		//- Inheritance: Set in the options (options.TypeNameHandling = TypeNameHandling.Objects)

		//4. Json by Hand
		JToken doc = JToken.Parse(readJson);
		foreach (JToken token in doc)
		{
			int maxV = token["MaxV"].Value<int>();
			Brand brand = (Brand) token["CarBrand"].Value<int>();

			Console.WriteLine(maxV);
			Console.WriteLine(brand);
			Console.WriteLine("----------------");
		}
	}

	static void XML()
	{
		List<Car> cars = new List<Car>
		{
			new Car(251, Brand.BMW),
			new RaceCar(274, Brand.BMW),
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

		//1. Serialization and Deserialization
		XmlSerializer xml = new XmlSerializer(cars.GetType());
		using (StreamWriter sw = new StreamWriter("Test.xml"))
		{
			xml.Serialize(sw, cars);
		}

		using (StreamReader sr = new StreamReader("Test.xml"))
		{
			List<Car> readCars = (List<Car>) xml.Deserialize(sr);
		}

		//2. Attributes
		//XmlIgnore
		//XmlAttribute: Converts a property to an attribute
		//XmlInclude: Inheritance

		//3. Xml by Hand
		XmlDocument doc = new XmlDocument();
		using StreamReader sr2 = new StreamReader("Test.xml");
		doc.Load(sr2);

		foreach (XmlNode node in doc.DocumentElement) //Skips the header and array node
		{
			//Basic XML
			//int maxV = int.Parse(node["MaxV"].InnerText);
			//Brand brand = Enum.Parse<Brand>(node["CarBrand"].InnerText);

			//Attribute Syntax
			int maxV = int.Parse(node.Attributes["MaxV"].Value);
			Brand brand = Enum.Parse<Brand>(node.Attributes["CarBrand"].Value);

			Console.WriteLine(maxV);
			Console.WriteLine(brand);
			Console.WriteLine("---------------------");
		}
	}
}

[DebuggerDisplay("Brand: {CarBrand}, MaxV: {MaxV} - {typeof(Car).FullName}")]
//[JsonDerivedType(typeof(Car), "C")]
//[JsonDerivedType(typeof(RaceCar), "RC")]
[XmlInclude(typeof(Car))]
[XmlInclude(typeof(RaceCar))]
public class Car
{
	[XmlAttribute]
	public int MaxV { get; set; }

	[XmlAttribute]
	public Brand CarBrand { get; set; }

	public Car(int maxV, Brand brand)
	{
		MaxV = maxV;
		CarBrand = brand;
	}

	public Car() { }

	//[JsonExtensionData]
	//public Dictionary<string, object> OtherData = [];
}

public enum Brand { Audi, BMW, VW }

public class RaceCar : Car
{
	public RaceCar()
	{
	}

	public RaceCar(int maxV, Brand brand) : base(maxV, brand)
	{
	}
}