// File:    LekService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class LekService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoDrugByPasswordException : Exception
    {
        public NoDrugByPasswordException()
        {
        }

        public NoDrugByPasswordException(string message) : base(message)
        {
        }

        public NoDrugByPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDrugByPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}