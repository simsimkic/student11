// File:    ReceptService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class ReceptService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoRecipeByPersonalNumberException : Exception
    {
        public NoRecipeByPersonalNumberException()
        {
        }

        public NoRecipeByPersonalNumberException(string message) : base(message)
        {
        }

        public NoRecipeByPersonalNumberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRecipeByPersonalNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}