namespace Learn_MVVM_Toolkit;



public class SaleProduct : Product
{
    public double Total { get;  set; }
    public SaleProduct(string name, double price, int count, double total) : base(name, price, count)
    {
        Total = total;
    }
    public SaleProduct(Product product) : base(product.Name,product.Price, product.Count )
    {
        Total = Price * Count;    
    }


}
