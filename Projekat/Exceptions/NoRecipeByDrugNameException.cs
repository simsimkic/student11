// File:    ReceptService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class ReceptService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoRecipeByDrugNameException : Exception
    {
        public NoRecipeByDrugNameException()
        {
        }

        public NoRecipeByDrugNameException(string message) : base(message)
        {
        }

        public NoRecipeByDrugNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRecipeByDrugNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}