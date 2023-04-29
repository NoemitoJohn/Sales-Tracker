namespace Learn_MVVM_Toolkit;



public class SaleProduct : Product
{
    public double Total { get;  set; }
    public SaleProduct(string name, double price, int count, double total) : base(name, count, price)
    {
        Total = total;
    }
    public SaleProduct(Product product) : base(product.Name, product.Count, product.Price)
    {
        Total = Price * Count;    
    }


}
