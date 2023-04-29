using Learn_MVVM_Toolkit.ObservableObjects;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;

namespace Learn_MVVM_Toolkit.Service;

public class DataBaseModel : IDataBaseModel
{
    public void CreateDatabase()
    {
        using (var connection = new SqliteConnection("Data Source = Test.db"))
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS Product (
                        ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Count INTEGER NOT NULL,
                        Price REAL NOT NULL
                    )";
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                Console.WriteLine("Failed to create Database. " + e.Message);
            }
        }
    }

    public Result InsertProduct(Product p)
    {
        Int32 result = 0;

        //TODO: Make sure to update this sqlite connection!!!!
        using (var connection = new SqliteConnection("Data Source = Test.db"))
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO Product (Name, Count, Price)
                      VALUES ($Name, $Count, $Price);
                      
                       SELECT CAST(last_insert_rowid() as int);
                    ";

                command.Parameters.AddWithValue("$Name", p.Name).SqliteType = SqliteType.Text;
                command.Parameters.AddWithValue("$Count", p.Count).SqliteType = SqliteType.Integer;
                command.Parameters.AddWithValue("$Price", p.Price).SqliteType = SqliteType.Real;

                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception e)
            { 
                Console.WriteLine("Failed to Insert data to Database. " + e.Message);
            }
        }

        return new Result(result);
    }

    public List<Product> GetAllProductAsList()
    {
        List<Product> products = new();

        using (var connection = new SqliteConnection("Data Source = Test.db"))
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"  SELECT ID, Name, Count, Price
                        FROM Product 
                    ";

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
        return new Product(
            r.GetInt32(r.GetOrdinal("ID")),
            r.GetString(r.GetOrdinal("Name")),
            r.GetInt32(r.GetOrdinal("Count")),
            r.GetDouble(r.GetOrdinal("Price"))
            );

    }

    public List<ProductObservable> GetAllObservableProductsAsList()
    {
        throw new NotImplementedException();

    }

    public void DeleteProduct(Product p)
    {
        throw new NotImplementedException();
    }


    public class Result
    {
        public Int32 ID { get; }

        public bool IsSuccess => ID > 0;

        public Result(Int32 iD)
        {
            ID = iD;
        }
    }

}
