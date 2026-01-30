using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Exceptions
{
    public class CustomConcurrencyException : CustomException
    {
        public CustomConcurrencyException()
        {
        }

        public CustomConcurrencyException(string message) : base(message)
        {
        }

        public CustomConcurrencyException(string message, DbUpdateConcurrencyException innerException) : base(message, innerException)
        {
        }
    }
}
