// File:    LekService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class LekService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoDrugsAvailableException : Exception
    {
        public NoDrugsAvailableException()
        {
        }

        public NoDrugsAvailableException(string message) : base(message)
        {
        }

        public NoDrugsAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDrugsAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}