using System;

namespace Insurance.Api.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string name, object value)
        : base(String.Format("Not Found: {0}={1}", name, value))
        {

        }
    }
}
