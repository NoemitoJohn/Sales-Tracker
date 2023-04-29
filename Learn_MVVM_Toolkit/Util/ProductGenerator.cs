using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.Util;

public class ProductGenerator
{
    static string[] names =
    {
        "Coke Mismo",
        "Royal Mismo",
        "Sprite Mismo",
        //
        "Coke Litro",
        "Royal Litro",
        "Sprite Litro",
        //
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

            Product p = new Product(names[NameIndex], RandomCount, double.Parse(RandomPrice.ToString()));
            products.Add(p);
        }

        return products;
    }
}
