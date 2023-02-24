// File:    KorisnikService.cs
// Created: Saturday, July 4, 2020 12:27:59 PM
// Purpose: Definition of Class KorisnikService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class InvalidUsernameException : Exception
    {
        public InvalidUsernameException()
        {
        }

        public InvalidUsernameException(string message) : base(message)
        {
        }

        public InvalidUsernameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidUsernameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}