﻿using OT.Assessment.Core;

namespace Exceptions
{
    internal class ValidationException : Exception
    {
        public ValidationException(string? message) : base(message)
        {
        }
    }
}