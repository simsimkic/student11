// File:    ReceptService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class ReceptService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoRecipeByDoctorNameException : Exception
    {
        public NoRecipeByDoctorNameException()
        {
        }

        public NoRecipeByDoctorNameException(string message) : base(message)
        {
        }

        public NoRecipeByDoctorNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRecipeByDoctorNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}