using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit;

public class Customer
{
    string _customerName;
    string _phoneNumber;
    string _address;

    public Customer(string customerName, string phoneNumber, string address)
    {
        _customerName = customerName;
        _phoneNumber = phoneNumber;
        _address = address;
    }
}
