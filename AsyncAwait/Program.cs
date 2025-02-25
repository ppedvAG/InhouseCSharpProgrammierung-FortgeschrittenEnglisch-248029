using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();

		//Toast();
		//Cup();
		//Coffee();

		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		///////////////////////////////////////////////////////////

		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Cup);
		//t2.Wait(); //Blocks the Main Thread
		//Task t3 = Task.Run(Coffee);
		//Task.WaitAll(t1, t3); //Blocks the Main Thread

		//Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////////

		//Task t1 = new Task(Toast); //Toast = 4 seconds
		//t1.ContinueWith(x => Console.WriteLine(sw.ElapsedMilliseconds));
		//t1.Start();

		//Task t2 = new Task(Cup); //Cup + Coffee = 3 seconds
		//t2.ContinueWith(x => Coffee());
		//t2.Start();

		//What if the toast task is not necessarily the last task?
		//Then the timer will be printed before the last task is completed (in this case the coffee task)
		//Solution(s): Complex ContinueWith trees, await

		///////////////////////////////////////////////////////////

		//Async/Await

		//async
		//If a method has the async keyword, and is not void, it will be executed as a task
		Task t = Test(); //Here the Task gets started automatically

		//The three return values:
		//- async void: Can use the await keyword, but cannot be awaited itself
		//- async Task: Can use the await keyword, and can be awaited itself
		//- async Task<T>: Can use the await keyword, can be awaited itself, and returns a value

		//await
		//Equivalent to t.Wait() and t.Result, but does not block the main thread
		//Generates a state machine in the background, to not block anything
		//If we await a task with a return value, then the await keyword will also return the value
		Task<int> rt = ReturnTest();
		await t; //Wait for t to complete (no return value)
		int x = await rt; //Wait for rt to complete (and save the result into a variable)
		Console.WriteLine(x);

		///////////////////////////////////////////////////////////

		//Breakfast example using async/await
		//Task t1 = ToastAsync();
		//Task t2 = CupAsync();
		//await t2;
		//Task t3 = CoffeeAsync();
		////await t3;
		////await t1;
		//await Task.WhenAll(t1, t3);

		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//1. Start tasks
		//2. Steps in between (optional)
		//3. await the task(s)

		///////////////////////////////////////////////////////////

		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Cup> t2 = CupObjectAsync();
		//Cup cup = await t2;
		//Task<Coffee> t3 = CoffeeObjectAsync(cup);
		//Toast toast = await t1;
		//Coffee coffee = await t3;

		//Breakfast b = new Breakfast(toast, coffee);

		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		///////////////////////////////////////////////////////////

		//Shortened
		Task<Toast> t1 = ToastObjectAsync();
		Task<Cup> t2 = CupObjectAsync();
		Task<Coffee> t3 = CoffeeObjectAsync(await t2);

		Breakfast b = new Breakfast(await t1, await t3);

		Console.WriteLine(sw.ElapsedMilliseconds); //4s
	}

	#region Synchronous
	static void Toast()
	{
		int x = Random.Shared.Next(1000, 4000);
		Console.WriteLine(x);
		Thread.Sleep(x);
		Console.WriteLine("Toast completed");
	}

	static void Cup()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Cup completed");
	}

	static void Coffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Coffee completed");
	}
	#endregion

	#region AsyncAwait examples
	/// <summary>
	/// async Task methods do not use return
	/// </summary>
	static async Task Test()
	{
		//Thread.Sleep(3000);
		Console.WriteLine("Test");
	}

	static async Task<int> ReturnTest()
	{
		//Thread.Sleep(3000);
		Console.WriteLine("Test");
		return Random.Shared.Next();
	}
	#endregion

	#region Asynchronous
	static async Task ToastAsync()
	{
		await Task.Delay(4000); //== Thread.Sleep(...)
		Console.WriteLine("Toast completed");
	}

	static async Task CupAsync()
	{
		await Task.Delay(1500); //== Thread.Sleep(...)
		Console.WriteLine("Cup completed");
	}

	static async Task CoffeeAsync()
	{
		await Task.Delay(1500); //== Thread.Sleep(...)
		Console.WriteLine("Coffee completed");
	}
	#endregion

	#region Asynchronous with objects
	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000); //== Thread.Sleep(...)
		Console.WriteLine("Toast completed");
		return new Toast();
	}

	static async Task<Cup> CupObjectAsync()
	{
		await Task.Delay(1500); //== Thread.Sleep(...)
		Console.WriteLine("Cup completed");
		return new Cup();
	}

	static async Task<Coffee> CoffeeObjectAsync(Cup c)
	{
		await Task.Delay(1500); //== Thread.Sleep(...)
		Console.WriteLine("Coffee completed");
		return new Coffee(c);
	}
	#endregion
}

public record Toast();

public record Cup();

public record Coffee(Cup c);

public record Breakfast(Toast t, Coffee c);