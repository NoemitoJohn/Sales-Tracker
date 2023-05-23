using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.Builder;

public interface IProductInfoBuilder
{
    public  ProductInfoBuilder SetID(int id);
    public  ProductInfoBuilder SetProductID(int productID);
    public  ProductInfoBuilder SetSold(int sold);
    public ProductInfoBuilder SetDescription(string description);

}
