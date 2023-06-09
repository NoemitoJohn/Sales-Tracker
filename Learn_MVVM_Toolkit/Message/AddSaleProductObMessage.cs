using CommunityToolkit.Mvvm.Messaging.Messages;
using Learn_MVVM_Toolkit.ObservableObjects;

namespace Learn_MVVM_Toolkit.Message;

public class AddSaleProductObMessage : ValueChangedMessage<SaleProductObservable>
{
    public AddSaleProductObMessage(SaleProductObservable value) : base(value)
    {
    }
}