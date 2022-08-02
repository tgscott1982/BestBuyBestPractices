using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices;
public class DapperProductRepository : IProductRepository
{
    private readonly IDbConnection _connection;
    //Constructor
    public DapperProductRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _connection.Query<Product>("SELECT * FROM Products;");
    }

    public void CreateProduct(string name, double price, int categoryID)
    {
        _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@Name, @Price, @CategoryID);",
        new { Name = name, Price = price, CategoryID = categoryID });
    }

    public void UpdateProduct(int productID, double price)
    {
        _connection.Execute("UPDATE products SET Price = @price WHERE ProductID = @ProductID;",
        new { Price = price, ProductID = productID });
    }

    public void DeleteProduct(int productID)
    {
        _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { ProductID = productID });

        _connection.Execute("DELETE FROM sales WHERE ProductID = @ProductID;",
           new { ProductID = productID });

        _connection.Execute("DELETE FROM products WHERE ProductID = @ProductID;",
           new { ProductID = productID });
    }

}
