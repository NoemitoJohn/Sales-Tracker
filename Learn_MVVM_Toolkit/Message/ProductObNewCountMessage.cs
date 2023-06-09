using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.Message;

public class ProductObNewCountMessage : ValueChangedMessage<ProductObNewCount>
{
    public ProductObNewCountMessage(ProductObNewCount value) : base(value)
    {
    }
}

public class ProductObNewCount
{
    public ProductObNewCount(int productId, int count)
    {
        ProductId = productId;
        Count = count;
    }

    public int ProductId { get; set; }
    public int Count { get; set; }
}