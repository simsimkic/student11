// File:    ReceptService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class ReceptService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class InvalidRecipeDrugException : Exception
    {
        public InvalidRecipeDrugException()
        {
        }

        public InvalidRecipeDrugException(string message) : base(message)
        {
        }

        public InvalidRecipeDrugException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRecipeDrugException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}