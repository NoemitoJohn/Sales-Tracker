using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.Builder;

public interface IProductBuilder
{

    public ProductBuilder SetID(int ID);
    public ProductBuilder SetName(string name);
    public ProductBuilder SetCost(double price);
    public ProductBuilder SetCount(int count);
    public ProductBuilder SetCategory(string category);

    public ProductBuilder SetPrice(double retailPrice);
    public Product Build();
}
