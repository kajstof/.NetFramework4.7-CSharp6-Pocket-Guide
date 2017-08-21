using System;

namespace CSharp6PocketReferenceBook
{
    partial class PaymentForm
    {
        partial void ValidatePayment(decimal amount)
        {
            if (amount > 100) Console.Write("Expensive!");
        }
    }
}
