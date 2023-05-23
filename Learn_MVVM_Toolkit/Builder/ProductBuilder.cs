using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.Builder;

public class ProductBuilder : IProductBuilder
{
    private Product product;

    private ProductInfoBuilder productInfoBuilder;
    public Product Product => product;
    public ProductInfoBuilder InfoBuilder => productInfoBuilder;

    public ProductBuilder()
    {
        product = new();
        productInfoBuilder = new();
    }

    public Product Build()
    {
        Product p = new(product.ID, product.Name, product.Count, product.Cost, product.Price, product.Category, productInfoBuilder.Info);
        return p;
    }

    public ProductBuilder SetCategory(string category)
    {
        Product p = new(product.ID, product.Name, product.Count, product.Cost, product.Price, category, productInfoBuilder.Info);
        product = p;
        return this;
    }

    public ProductBuilder SetCount(int count)
    {
        Product p = new(product.ID, product.Name, count, product.Cost, product.Price, product.Category, productInfoBuilder.Info);
        product = p;
        return this;
    }

    public ProductBuilder SetID(int ID)
    {
        Product p = new(ID, product.Name, product.Count, product.Cost, product.Price, product.Category, productInfoBuilder.Info);
        product = p;
        return this;
    }

    public ProductBuilder SetName(string name)
    {
        Product p = new(product.ID, name, product.Count, product.Cost, product.Price, product.Category, productInfoBuilder.Info);
        product = p;

        return this;
    }

    public ProductBuilder SetCost(double cost)
    {
        Product p = new(product.ID, product.Name, product.Count, cost, product.Price, product.Category, productInfoBuilder.Info);
        product = p;
        return this;
    }

    public ProductBuilder SetPrice(double price)
    {
        Product p = new(product.ID, product.Name, product.Count, product.Cost, price, product.Category, productInfoBuilder.Info);
        product = p;
        return this;
    }
}
