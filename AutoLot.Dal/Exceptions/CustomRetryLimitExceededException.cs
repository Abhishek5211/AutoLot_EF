using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Exceptions
{
    public class CustomRetryLimitExceededException : CustomException
    {
        public CustomRetryLimitExceededException()
        {
        }

        public CustomRetryLimitExceededException(string message) : base(message)
        {
        }

        public CustomRetryLimitExceededException(string message, RetryLimitExceededException innerException) : base(message, innerException)
        {
        }
    }
}
