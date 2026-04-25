// See https://aka.ms/new-console-template for more information
using CSharpChallenges.UI;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
//"MyDbConnection": "Server=localhost\\SQLEXPRESS;Database=inventory_system;User Id=franky;Password=password123;TrustServerCertificate=True;" //laptop


var display = new Menu();
//display.Display();



var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();

string conn = config.GetConnectionString("DefaultConnection");

Console.WriteLine(conn);

using (SqlConnection connection = new SqlConnection(conn))
{
    connection.Open();

    string query = "SELECT * FROM TblUser";

    using (SqlCommand command = new SqlCommand(query, connection))
    using (SqlDataReader reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["UserId"]}, Name: {reader["FirstName"]}");
        }
    }
}