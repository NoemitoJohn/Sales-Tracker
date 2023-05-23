using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.Builder;

public class ProductInfoBuilder : IProductInfoBuilder
{
    private Product.Info info;

    public Product.Info Info => info;

    public ProductInfoBuilder()
	{
		info = new();
	}

    public ProductInfoBuilder SetDescription(string description)
    {
        Product.Info i = new(info.ID, info.ProductID, info.Sold, description);
        info = i;
        return this;
    }

    public ProductInfoBuilder SetID(int id)
    {
        Product.Info i = new(id, info.ProductID, info.Sold, info.Description);
        info = i;
        return this;
    }


    public ProductInfoBuilder SetProductID(int productID)
    {
        Product.Info i = new(info.ID, productID, info.Sold, info.Description);
        info = i;
        return this;    
    }

    public ProductInfoBuilder SetSold(int sold)
    {
        Product.Info i = new(info.ID, info.ProductID, sold, info.Description);
        info = i;
        return this;
    }
}
