using CSharpChallenges.Data;
using CSharpChallenges.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();

string conn = config.GetConnectionString("DefaultConnection");

Console.WriteLine(conn);

// 🔥 CREATE DB OPTIONS (THIS IS WHAT YOU ARE MISSING)
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(conn)
    .Options;

// 🔥 CREATE DB CONTEXT
using var db = new AppDbContext(options);

// 🔥 LINQ QUERY
var users = db.TblUser.ToList();

foreach (var u in users)
{
    Console.WriteLine(u.FullName);
}
// test branch


var login = new Login(db);

Console.Write("Enter First Name: ");
string firstName = Console.ReadLine();

Console.Write("Enter PIN: ");
string inputPin = Console.ReadLine();

if (login.LoginUser(firstName, inputPin))
{
    Console.WriteLine("Login successful!");
}
else
{
    Console.WriteLine("Invalid credentials.");
}



