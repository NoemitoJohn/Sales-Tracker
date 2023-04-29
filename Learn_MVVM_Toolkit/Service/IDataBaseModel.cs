using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Learn_MVVM_Toolkit.Service.DataBaseModel;

namespace Learn_MVVM_Toolkit.Service;

public interface IDataBaseModel
{
    public void CreateDatabase();

    public Result InsertProduct(Product p);

    public List<Product> GetAllProductAsList();


    public List<ProductObservable> GetAllObservableProductsAsList();

    public void DeleteProduct(Product p);
}
