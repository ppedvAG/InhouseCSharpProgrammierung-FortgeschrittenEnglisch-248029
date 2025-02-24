using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DelegateWPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		Task t = new Task(Run);
		t.Start();

		t.Wait(); //Code stops right here
				  //Instead of t.Wait() you should always use await

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}
	}

	public void Run()
	{
		Console.WriteLine("Task started");
		Thread.Sleep(3000);
		Console.WriteLine("Task completed");
	}
}