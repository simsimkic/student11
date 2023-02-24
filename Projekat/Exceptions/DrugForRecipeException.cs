// File:    LekService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class LekService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class DrugForRecipeException : Exception
    {
        public DrugForRecipeException()
        {
        }

        public DrugForRecipeException(string message) : base(message)
        {
        }

        public DrugForRecipeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DrugForRecipeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}