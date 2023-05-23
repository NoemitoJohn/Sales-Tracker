using Learn_MVVM_Toolkit.Builder;
using Learn_MVVM_Toolkit.ObservableObjects;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Learn_MVVM_Toolkit.Service;

public class DataBaseModel : IDataBaseModel
{


    public void CreateDatabase()
    {
        using var connection = new SqliteConnection("Data Source = Test.db");

        try
        {
            connection.Open();

            using var command = connection.CreateCommand();

            //Change the "Price" to Cost and "RetailPrice" to Price

            command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Product (
                        ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Count INTEGER NOT NULL,
                        Price REAL NOT NULL, 
                        RetailPrice REAL NOT NULL, 
                        Category TEXT);
                        
                        CREATE TABLE IF NOT EXISTS ProductInfo (
                        ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        ProductID INTEGER NOT NULL,
                        Sold INTEGER NOT NULL,
                        Description TEXT)";
            //TODO: Return the resut for checking if the table is created   
            int result = command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to create Tables. " + e.Message);
        }
    }

    //    CREATE TABLE "ProductInfo" (
    //	"ID"	INTEGER NOT NULL UNIQUE,
    //	"ProductID"	INTEGER NOT NULL,
    //	"Sold"	INTEGER,
    //	"Description"	TEXT,
    //	PRIMARY KEY("ID" AUTOINCREMENT)
    //)

    //    CREATE TABLE "Product" (
    //	"ID"	INTEGER NOT NULL UNIQUE,
    //	"Name"	TEXT NOT NULL,
    //	"Count"	INTEGER NOT NULL,
    //	"Price"	REAL NOT NULL,
    //	"RetailPrice"	REAL NOT NULL,
    //	"Category"	TEXT,
    //	PRIMARY KEY("ID" AUTOINCREMENT)
    //)

    /// <summary>
    /// Insert a product to the database and return the product inserted result 
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public Product InsertProductTransaction(Product product)
    {
        Product tempProduct = null;

        using (var connection = new SqliteConnection("Data Source = Test.db"))
        {
            try
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var SqlCommand = connection.CreateCommand();
                    SqlCommand.CommandText =
                        @"INSERT INTO Product (Name, Count, Price, RetailPrice, Category)
                        VALUES ($name, $count, $price, $retailprice, $category)";
                    SqlCommand.Parameters.AddWithValue("$name", product.Name).SqliteType = SqliteType.Text;
                    SqlCommand.Parameters.AddWithValue("$count", product.Count).SqliteType = SqliteType.Integer;
                    SqlCommand.Parameters.AddWithValue("$price", product.Cost).SqliteType = SqliteType.Real;
                    SqlCommand.Parameters.AddWithValue("$retailprice", product.Price).SqliteType = SqliteType.Real;
                    SqlCommand.Parameters.AddWithValue("$category", product.Category).SqliteType = SqliteType.Text;

                    int affectedRow = SqlCommand.ExecuteNonQuery();

                    if (affectedRow <= 0) //TODO: Create a Error and Success Information
                        return tempProduct;

                    SqlCommand.CommandText = @"SELECT last_insert_rowid()";

                    int productInsertedID = Convert.ToInt32(SqlCommand.ExecuteScalar());

                    if (productInsertedID <= 0) return tempProduct;


                    SqlCommand.CommandText =
                        @"INSERT INTO ProductInfo (productid, sold, description)
                        VALUES ($productID, $sold, $description)";
                    SqlCommand.Parameters.AddWithValue("$productID", productInsertedID);
                    SqlCommand.Parameters.AddWithValue("$sold", product.Information.Sold);
                    SqlCommand.Parameters.AddWithValue("$description", product.Information.Description);

                    int result = SqlCommand.ExecuteNonQuery();

                    if (result <= 0) return tempProduct;

                    SqlCommand.CommandText = @"SELECT last_insert_rowid()";

                    int productInfoID = Convert.ToInt32(SqlCommand.ExecuteScalar());

                    if (productInfoID > 0)
                    {
                        tempProduct = new Product(productInsertedID, product.Name, product.Count, product.Cost, product.Price, product.Category, new Product.Info(productInfoID, productInsertedID, product.Information.Sold, product.Information.Description));
                        transaction.Commit();
                    }
                    else transaction.Rollback();
                }
                connection.Close();
            }
            catch (Exception)
            {

            }

        }
        return tempProduct;
    }
    //    SELECT Product.ID, Product.name, Product.Count, Product.Price, ProductInfo.ID, ProductInfo.Sold, ProductInfo.Description
    //    FROM ProductInfo
    //    INNER JOIN Product ON Product.ID = ProductInfo.ProductID
    public List<Product> GetAllProductAsList()
    {
        List<Product> products = new();

        using (var connection = new SqliteConnection("Data Source = Test.db"))
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand(); //TODO: Update to get all the info
                command.CommandText = @"
                    SELECT Product.ID, Product.Name, Product.Count, Product.Price, Product.RetailPrice, Product.Category, ProductInfo.Sold, ProductInfo.Description
                    FROM ProductInfo 
                    INNER JOIN Product ON Product.ID = ProductInfo.ProductID";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        products.Add(DbObjectProductConverter(reader));
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                connection.Close();
                Console.WriteLine("Failed to Insert data to Database. " + e.Message);
            }

            connection.Close();
        }

        return products;

    }

    private Product DbObjectProductConverter(IDataReader r)
    {
        // TOD: Update to new 
        ProductBuilder builder = new ProductBuilder();

        builder
            .SetID(r.GetInt32(r.GetOrdinal("ID")))
            .SetName(r.GetString(r.GetOrdinal("Name")))
            .SetCount(r.GetInt32(r.GetOrdinal("Count")))
            .SetCost(r.GetDouble(r.GetOrdinal("Price")))
            .SetPrice(r.GetDouble(r.GetOrdinal("RetailPrice")))
            .SetCategory(r.GetString(r.GetOrdinal("Category")))

            .InfoBuilder
                .SetSold(r.GetInt32(r.GetOrdinal("Sold")))
                .SetProductID(r.GetInt32(r.GetOrdinal("ID")))
                .SetDescription(r.GetString(r.GetOrdinal("Description")));

        return builder.Build();

    }

    public List<ProductObservable> GetAllObservableProductsAsList()
    {
        throw new NotImplementedException();

    }

    public void DeleteProduct(Product p)
    {
        throw new NotImplementedException();
    }

    public void InsertSold(Sold order)
    {
        //TODO: Implement try catch and display error In a dialog box?????
        using var connection = new SqliteConnection("Data Source = Test.db");

        connection.Open();

        using var transaction = connection.BeginTransaction();

        using var command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO Orders (OrderDate, TYPE, TotalCost, TotalRevenue, TotalProfit)
                                    VALUES ($date, $type, $totalC, $totalR, $totalP)";

        command.Parameters.AddWithValue("$date", order.Date.ToString()).SqliteType = SqliteType.Text;
        command.Parameters.AddWithValue("$type", order.Type.ToString()).SqliteType = SqliteType.Text;
        command.Parameters.AddWithValue("$totalC", order.TotalCosts).SqliteType = SqliteType.Real;
        command.Parameters.AddWithValue("$totalR", order.TotalRevenue).SqliteType = SqliteType.Real;
        command.Parameters.AddWithValue("$totalP", order.TotalProfit).SqliteType = SqliteType.Real;

        int resut = command.ExecuteNonQuery();

        if (resut < 1) return;

        command.CommandText = @"SELECT last_insert_rowid();";

        int id = Convert.ToInt32(command.ExecuteScalar());

        if (id <= 0) return;

        command.Parameters.Clear();

        int insertedSoldCount = 0;

        for (int i = 0; i < order.Orders.Count; i++)
        {
            command.CommandText = @"INSERT INTO SoldProduct (OrdID, Name, Count, TCost, TPrice, TProfit)
                                    VALUES ($ID, $name, $count, $Tcost, $Tprice, $Tprofit)";

            command.Parameters.AddWithValue("$ID", id).SqliteType = SqliteType.Integer;
            command.Parameters.AddWithValue("$name", order.Orders[i].Name).SqliteType = SqliteType.Text;
            command.Parameters.AddWithValue("$count", order.Orders[i].Count).SqliteType = SqliteType.Integer;
            command.Parameters.AddWithValue("$Tcost", order.Orders[i].TotalCost).SqliteType = SqliteType.Real;
            command.Parameters.AddWithValue("$Tprice", order.Orders[i].TotalPrice).SqliteType = SqliteType.Real;
            command.Parameters.AddWithValue("$Tprofit", order.Orders[i].TotalProfit).SqliteType = SqliteType.Real;

            int insertedResult = command.ExecuteNonQuery();

            command.Parameters.Clear();

            if (insertedResult > 0) insertedSoldCount = i;
        }

        if (insertedSoldCount == (order.Orders.Count - 1))
        {
            transaction.Commit();
        }
    }

    public List<Sold> GetAllSoldOrders()
    {
        List<Sold> soldList = new();
        List<SoldProductObservable> soldProducts = new();
        using var connection = new SqliteConnection("Data Source = Test.db");

        connection.Open();
        using var transaction = connection.BeginTransaction();
        using var selectOrdersCommand = connection.CreateCommand();

        selectOrdersCommand.CommandText = @"SELECT * FROM Orders";

        using (var orderReader = selectOrdersCommand.ExecuteReader())
        {
            while (orderReader.Read())
            {
                int id = orderReader.GetInt32(orderReader.GetOrdinal("OrderID"));
                string date = orderReader.GetString(orderReader.GetOrdinal("OrderDate"));
                string type = orderReader.GetString(orderReader.GetOrdinal("TYPE"));
                double totalCost = orderReader.GetDouble(orderReader.GetOrdinal("TotalCost"));
                double totalRevenue = orderReader.GetDouble(orderReader.GetOrdinal("TotalRevenue"));
                double totalProfit = orderReader.GetDouble(orderReader.GetOrdinal("TotalProfit"));

                if (id >= 0)
                {
                    using var selectSoldProductCommand = connection.CreateCommand();
                    selectSoldProductCommand.CommandText = @"Select * FROM SoldProduct WHERE OrdID = $id";
                    selectSoldProductCommand.Parameters.AddWithValue("$id", id);

                    using var soldProductReader = selectSoldProductCommand.ExecuteReader();

                    while (soldProductReader.Read())
                    {
                        string name = soldProductReader.GetString(soldProductReader.GetOrdinal("name"));
                        int count = soldProductReader.GetInt32(soldProductReader.GetOrdinal("Count"));
                        double tCost = soldProductReader.GetDouble(soldProductReader.GetOrdinal("TCost"));
                        double tPrice = soldProductReader.GetDouble(soldProductReader.GetOrdinal("TPrice"));
                        double tProfit = soldProductReader.GetDouble(soldProductReader.GetOrdinal("TProfit"));

                        SoldProductObservable soldProduct = new(name, count, tCost, tPrice, tProfit);
                        soldProducts.Add(soldProduct);
                    }
                    Sold sold = new Sold(DateTime.Parse(date), Order.GetType(type), totalCost, totalRevenue, totalProfit, new List<SoldProductObservable>(soldProducts));
                    soldList.Add(sold);
                    soldProducts.Clear();
                }
            }

        }

        return soldList;

        //throw new NotImplementedException();
    }
}
