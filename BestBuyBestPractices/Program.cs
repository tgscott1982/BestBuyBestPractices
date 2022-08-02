using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using BestBuyBestPractices;

#region Config Code

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
string connString = config.GetConnectionString("DefaultConnection");
IDbConnection conn = new MySqlConnection(connString);
#endregion

#region Department dapper method use and printout

//var deptRepo = new DapperDepartmentRepository(conn);
//var departments = deptRepo.GetAllDepartments();
//Console.WriteLine("Here is a list of Departments currently in your database:\n");

//foreach (var d in departments)
//{
//    Console.WriteLine($"Department ID: {d.DepartmentID} || Department Name: {d.Name}\n");
//}

//Console.WriteLine("Please type the name of your new Department.\n");

//var newDepartment = Console.ReadLine();
//deptRepo.InsertDepartment(newDepartment);
//departments = deptRepo.GetAllDepartments();

//Console.WriteLine("\nYour new Department has been added.\n");
//Console.WriteLine("Here is an updated list of Departments in your database.\n");

//foreach(var d in departments)
//{
//    Console.WriteLine($"Department ID: {d.DepartmentID} || Department Name: {d.Name}\n");
//}

#endregion


var prodRepo = new DapperProductRepository(conn);
var products = prodRepo.GetAllProducts();
Console.WriteLine("Here is a list of all Products currently in your database:");

foreach(var p in products)
{
    Console.WriteLine($"Product ID: {p.ProductID} || Name: {p.Name}");
}
Console.WriteLine();
Console.WriteLine("Let's create a new Product in the database.");
Console.WriteLine("Please enter a name for your new Product.");

var newProductName = Console.ReadLine();

Console.WriteLine("Please enter a price for your new Product.");

var newProductPrice = double.Parse(Console.ReadLine());

prodRepo.CreateProduct(newProductName, newProductPrice, 10);

products = prodRepo.GetAllProducts();

Console.WriteLine("Here is an updated Product list. Your new item should be automatically added to the end of the list.");
Console.WriteLine("Be sure to make note of the productID of your new product. We'll update your product in a moment, using the productID.");

foreach (var p in products)
{
    Console.WriteLine($"Product ID: {p.ProductID} || Name: {p.Name} || Price: {p.Price}");
}

Console.WriteLine("Please enter the productID from your new item so we can update its Price.");
var prodID = int.Parse(Console.ReadLine());
Console.WriteLine("Please enter an updated Price for your new item.");
var newProdPrice = double.Parse(Console.ReadLine());
prodRepo.UpdateProduct(prodID, newProdPrice);
products = prodRepo.GetAllProducts();

Console.WriteLine("Here is an updated product list for you to verify the changes.");
foreach( var p in products)
{
    Console.WriteLine($"Product ID: {p.ProductID} || Name: {p.Name} || Price: {p.Price}");
}

Console.WriteLine("Finally, we are going to delete your product entry from the database.");
Console.WriteLine("Please entere the productID of your product to be deleted.");
prodID = int.Parse(Console.ReadLine());
prodRepo.DeleteProduct(prodID);
products = prodRepo.GetAllProducts();

Console.WriteLine("Here is an updated product list for you to verify the changes.");
foreach (var p in products)
{
    Console.WriteLine($"Product ID: {p.ProductID} || Name: {p.Name} || Price: {p.Price}");
}

Console.WriteLine();
Console.WriteLine("All done! Your database should be back to the way it was!");