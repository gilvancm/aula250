using System;
namespace SalesWebMvc247.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message) //  base(message) ----- herda da classe superio
        {
        }
    }
}
