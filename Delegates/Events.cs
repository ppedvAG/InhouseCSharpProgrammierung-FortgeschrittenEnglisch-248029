using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Delegates;

/// <summary>
/// Events
/// 
/// Interface for other developers
/// The developer of the event itself defines the event, using the event keyword + a delegate
/// The user of the class where the event is defined, attaches a method to the event
/// Then if the event gets fired, the attached method gets called
/// 
/// Two sides:
/// - Developer of the event: Defines the event itself, and fires it when required
/// - User of the event: Attaches a method, which gets run when the event gets fired
/// 
/// Three parts:
/// - The event itself
/// - The running/firing of the event
/// - The attaching of the method
/// </summary>
public class Events
{
	public event EventHandler TestEvent; //Developer side

	public event EventHandler<int> IntEvent;

	public event Action<int> ActionEvent; //Action is also possible here

	static void Main(string[] args) => new Events().Run();

	public void Run()
	{
		TestEvent += Events_TestEvent; //User side
		TestEvent(this, EventArgs.Empty); //Developer side

		///////////////////////////////////////////////////////

		IntEvent += Events_IntEvent;
		IntEvent(this, 10);

		///////////////////////////////////////////////////////

		ActionEvent += Events_ActionEvent;
		ActionEvent?.Invoke(20);

		///////////////////////////////////////////////////////

		//Practical example
		//ObservableCollection
		ObservableCollection<int> ints = [];
		ints.CollectionChanged += Ints_CollectionChanged;
		ints.Add(1);
		ints.Add(2);
		ints.Add(3);
		ints.Remove(1);
	}

	private void Ints_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		switch (e.Action)
		{
			case NotifyCollectionChangedAction.Add:
				Console.WriteLine($"Item added: {e.NewItems[0]}");
				break;
			case NotifyCollectionChangedAction.Remove:
				Console.WriteLine($"Item removed: {e.OldItems[0]}");
				break;
		}
	}

	/// <summary>
	/// User side
	/// 
	/// The user defines the code, which gets run, when the event is fired
	/// </summary>
	private void Events_TestEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("TestEvent fired");
	}

	/// <summary>
	/// Method with an int parameter, because the generic is the int type
	/// </summary>
	private void Events_IntEvent(object? sender, int e)
	{
		Console.WriteLine(e * 2);
	}

	private void Events_ActionEvent(int obj)
	{
		Console.WriteLine($"The number is: {obj}");
	}
}