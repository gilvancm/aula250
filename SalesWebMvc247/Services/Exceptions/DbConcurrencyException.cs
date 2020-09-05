﻿using System;

namespace SalesWebMvc247.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string message) : base(message)
        {
        }

    }
}
