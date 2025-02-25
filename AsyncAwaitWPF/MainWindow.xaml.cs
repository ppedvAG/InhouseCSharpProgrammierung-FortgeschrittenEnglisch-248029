using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		//Task: Print the numbers from 0 to 99 into a textfield, with a delay between each of the numbers
		for (int i = 0; i < 100; i++)
		{
			Info.Text += i + "\n";
			Scroll.ScrollToEnd();

			Thread.Sleep(25);
		}
	}

	private void Button_Click_TaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Dispatcher.Invoke(() => //Dispatcher: Subcomponent of every UI component; is used, to put code from sidetasks/-threads onto this component itself
				{
					Info.Text += i + "\n";
					Scroll.ScrollToEnd();
				});

				Thread.Sleep(25);
			}
		});
	}

	private async void Button_Click_Async(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Info.Text += i + "\n";
			Scroll.ScrollToEnd();

			//await Task.Run(() => Thread.Sleep(25));
			await Task.Delay(25);
		}
	}

	private async void Button_Click_AsyncDataSource(object sender, RoutedEventArgs e)
	{
		AsyncDataSource data = new();
		await foreach (int x in data.GenerateNumbers()) //await foreach: Regular foreach loop, but has to wait for each next value coming out of the IAsyncEnumerable
		{
			Info.Text += x + "\n";
		}
	}

	private async void Request(object sender, RoutedEventArgs e)
	{
		//1. Start task(s)
		//2. Steps in between (optional)
		//3. await the task(s)

		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";
		using HttpClient client = new();

		//Start task
		Task<HttpResponseMessage> request = client.GetAsync(url); //Start request

		//Steps in between
		Info.Text = "Loading text...";
		ReqButton.IsEnabled = false;

		//await the task
		HttpResponseMessage response = await request;

		if (!response.IsSuccessStatusCode)
		{
			Info.Text = "Loading failed";
			ReqButton.IsEnabled = true;
			return;
		}

		//Request was successful
		
		//Start task
		Task<string> content = response.Content.ReadAsStringAsync();

		//Steps in between
		Info.Text = "Reading text...";

		//await the task
		string text = await content;

		Info.Text = text;
		ReqButton.IsEnabled = true;
	}
}