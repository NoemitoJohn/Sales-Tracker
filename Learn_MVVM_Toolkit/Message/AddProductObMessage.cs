using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.Message;

public class AddProductObMessage : ValueChangedMessage<Product>
{
    public AddProductObMessage(Product value) : base(value)
    {
    }
}
