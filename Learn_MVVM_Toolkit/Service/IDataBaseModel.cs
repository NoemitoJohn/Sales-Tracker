using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Learn_MVVM_Toolkit.Service.DataBaseModel;

namespace Learn_MVVM_Toolkit.Service;

public interface IDataBaseModel
{
    public void CreateDatabase();

   // public Result InsertProduct(Product p);

    public List<Product> GetAllProductAsList();

    public Product InsertProductTransaction(Product product);

    public List<ProductObservable> GetAllObservableProductsAsList();

    public void DeleteProduct(Product p);

    public void InsertSold(Sold order);

    public List<Sold> GetAllSoldOrders();
    public int UpdateProductCount(int id, double newCount); 
    public int UpdateProductSold(int id, double newSold); 

    public Product UpdateProduct(Product product);
    public ObservableCollection<string> GetAllCategory();

}
