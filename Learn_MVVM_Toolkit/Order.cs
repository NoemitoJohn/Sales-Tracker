using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit;

public class Order
{
    DateTime date;
    Customer customer => new Customer("John", "090909090", "Tagoloan");
    IList<Product> products;
    double total;
    double income;  
}
