using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace DelegateWPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

	private async void Button_Click(object sender, RoutedEventArgs e)
	{
		await Task.Run(() => Enumerable.Range(0, 2_000_000_000).ToList());
		Output.Text = "Completed number generation";
	}
}