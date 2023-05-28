using Learn_MVVM_Toolkit.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.Util;

public class ProductGenerator
{
    public static string[] names =
    {
        "Product 1",
        "Product 2",
        "Product 3",
        "Product 4",
        "Product 5",
        "Product 6",
        "Product 7",
        "Product 8",
        "Product 9",
        "Product 10",
        "Product 11",
        //
    };
    public static string[] description =
    {
        "Description 1",
        "Description 2",
        "Description 3",
        "Description 4",
        "Description 5",
        "Description 6",
        "Description 6",
        "Description 7",
        "Description 8",
        "Description 9",
        "Description 10",
    };
    public static string[] category =
    {
        "Category 1",
        "Category 2",
        "Category 3",
        "Category 4",
        "Category 5",
        "Category 6",
        "Category 6",
        "Category 7",
        "Category 8",
        "Category 9",
        "Category 10",
    };

    public static List<Product> CreateFakeProduct(int count)
    {
        List<Product> products = new List<Product>();


        Random random = new Random();

        for (int i = 1; i < count + 1; i++)
        {
            int NameIndex = random.Next(names.Length);
            int RandomCount = random.Next(50, 101);
            int RandomPrice = random.Next(20, 51);
            int RandomDesc = random.Next(description.Length);
            int RandomCat = random.Next(description.Length);
            //TODO: update to new Constructor
            
            ProductBuilder builder = new ProductBuilder();
            builder
                .SetName(names[NameIndex])
                .SetCost(RandomPrice)
                .SetCount(RandomCount)
                .SetPrice(RandomPrice + 10)
                .SetCategory(category[RandomCat])
                
                .InfoBuilder
                    .SetDescription(description[RandomDesc])
                    .SetSold(0);

            products.Add(builder.Build());
            StringBuilder sb = new StringBuilder();
            
        }

        return products;
    }
}
