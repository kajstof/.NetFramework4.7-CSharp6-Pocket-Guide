using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    partial class PaymentForm
    {
        partial void ValidatePayment(decimal amount)
        {
            if (amount > 100) Console.Write("Expensive!");
        }
    }
}
