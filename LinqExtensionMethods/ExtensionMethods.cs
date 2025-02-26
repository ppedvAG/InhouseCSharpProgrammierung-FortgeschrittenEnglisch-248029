using System;

namespace LinqExtensionMethods;

static class ExtensionMethods
{
	public static int Crosssum(this int x)
	{
		//int sum = 0;
		//string numAsString = x.ToString();
		//for (int i = 0; i < numAsString.Length; i++)
		//{
		//	sum += (int) char.GetNumericValue(numAsString[i]);
		//}
		//return sum;

		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	//public static IEnumerable<TResult> Flatten<TSource, TResult>(this IEnumerable<TSource> objects)
	//{
	//	return objects.SelectMany(e => e);
	//}
}
