using CSharpChallenges;
using CSharpChallenges.Data;
using CSharpChallenges.Models;
using CSharpChallenges.Services;
using CSharpChallenges.UI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();

string conn = config.GetConnectionString("DefaultConnection");

// 🔥 CREATE DB OPTIONS (THIS IS WHAT YOU ARE MISSING)
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(conn)
    .Options;

// 🔥 CREATE DB CONTEXT
using var db = new AppDbContext(options);

var loginService = new LoginService(db);
var balanceService = new BalanceService(db);
var menu = new Menu();

var app = new App(loginService, menu, balanceService);
app.Run();