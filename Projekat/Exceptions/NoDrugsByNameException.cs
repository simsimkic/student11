// File:    LekService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class LekService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoDrugsByNameException : Exception
    {
        public NoDrugsByNameException()
        {
        }

        public NoDrugsByNameException(string message) : base(message)
        {
        }

        public NoDrugsByNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDrugsByNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}