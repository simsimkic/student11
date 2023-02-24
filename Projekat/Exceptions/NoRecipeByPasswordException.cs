// File:    ReceptService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class ReceptService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoRecipeByPasswordException : Exception
    {
        public NoRecipeByPasswordException()
        {
        }

        public NoRecipeByPasswordException(string message) : base(message)
        {
        }

        public NoRecipeByPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRecipeByPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}