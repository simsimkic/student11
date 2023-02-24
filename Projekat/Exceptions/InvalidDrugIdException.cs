// File:    LekService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class LekService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class InvalidDrugIdException : Exception
    {
        public InvalidDrugIdException()
        {
        }

        public InvalidDrugIdException(string message) : base(message)
        {
        }

        public InvalidDrugIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDrugIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}