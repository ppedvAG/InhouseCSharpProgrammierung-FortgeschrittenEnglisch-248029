using DatabaseDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Windows;

namespace DatabaseDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

	/// <summary>
	/// Two possibilities of loading Data:
	/// - Plain DB Connection (ADO.NET, DB Driver)
	/// - EntityFramework
	/// 
	/// 1. Plain DB Connection
	/// - DB Driver
	/// - SqlServer: Microsoft.Data.SqlClient
	/// 
	/// 2. EntityFramework
	/// - Microsoft.EntityFrameworkCore
	/// - Microsoft.EntityFrameworkCore.SqlServer
	/// - Microsoft.EntityFrameworkCore.Design
	/// - Microsoft.EntityFrameworkCore.Tools
	/// - Extension: EF Core Power Tools (optional)
	/// 
	/// Right click the project -> EF Core Power Tools -> Reverse Engineer
	/// 
	/// Important classes:
	/// - Context: Enables DB access using DbSets and Linq
	/// - Model: Representations of the DB Tables, where every column is a property with DataAnnotations
	/// </summary>
	private async void Button_Click(object sender, RoutedEventArgs e)
	{
		//using SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Northwind;Integrated Security=True;Encrypt=False");

		//Task open = connection.OpenAsync();
		//await open;

		//if (open.IsFaulted)
		//	MessageBox.Show("Connection could not be opened");

		//using SqlCommand command = connection.CreateCommand();
		//command.CommandText = "SELECT * FROM Customers";

		//using SqlDataReader reader = command.ExecuteReader();

		//List<object[]> rows = [];
		//while (await reader.ReadAsync())
		//{
		//	object[] columns = new object[reader.VisibleFieldCount]; //VisibleFieldCount: Amount of columns in the table
		//	reader.GetValues(columns); //Writes the data into the array; takes the array as a reference
		//	rows.Add(columns);
		//}
		//DG.ItemsSource = rows;

		//////////////////////////////////////////////////////////////////////////

		NorthwindContext db = new();

		List<Customer> customers = await db.Customers.Where(e => e.Country == "UK").ToListAsync(); //SELECT * FROM Customers WHERE Country = 'UK'

		DG.ItemsSource = customers;
	}
}