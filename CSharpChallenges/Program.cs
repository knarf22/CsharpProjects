using CSharpChallenges.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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