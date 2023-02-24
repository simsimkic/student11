// File:    LekService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class LekService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoDrugsByPriceException : Exception
    {
        public NoDrugsByPriceException()
        {
        }

        public NoDrugsByPriceException(string message) : base(message)
        {
        }

        public NoDrugsByPriceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDrugsByPriceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}