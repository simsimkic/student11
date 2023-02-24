// File:    ReceptService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class ReceptService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoRecipesAvailableException : Exception
    {
        public NoRecipesAvailableException()
        {
        }

        public NoRecipesAvailableException(string message) : base(message)
        {
        }

        public NoRecipesAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRecipesAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}