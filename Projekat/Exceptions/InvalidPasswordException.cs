// File:    KorisnikService.cs
// Created: Saturday, July 4, 2020 12:27:59 PM
// Purpose: Definition of Class KorisnikService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
        {
        }

        public InvalidPasswordException(string message) : base(message)
        {
        }

        public InvalidPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}