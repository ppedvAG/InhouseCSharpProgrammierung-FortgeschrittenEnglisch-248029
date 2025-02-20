namespace Generics;

public class Constraints
{
	public Constraints()
	{

	}

	public T Test<T>(T param) where T : new()
	{
		T obj = new T();
		return obj;
	}
}

public class ExampleClass;

public interface IExample;

//////////////////////////////////////////////////////////////

public class DataStore1<T> where T : ExampleClass; //ExampleClass has to be the superclass (or the class itself)

public class DataStore2<T> where T : IExample; //DataStore2 has to have the IExample Interface

public class DataStore3<T> where T : class; //T has to be a reference type (a class)

public class DataStore4<T> where T : struct; //T has to be a value type (a struct)

public class DataStore5<T> where T : new(); //T must contain a default constructor

public class DataStore6<T> where T : Enum;

public class DataStore7<T> where T : Delegate;

public class DataStore8<T> where T : unmanaged; //Collection of types (int, double, bool, nint, nuint, ...)

public class DataStore9<T> where T : notnull; //T is not allowed to be nullable

public class DataStore10<T1, T2>
	where T1 : class, new()
	where T2 : struct
{

}