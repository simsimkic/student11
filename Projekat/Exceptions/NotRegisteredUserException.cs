// File:    KorisnikService.cs
// Created: Saturday, July 4, 2020 12:27:59 PM
// Purpose: Definition of Class KorisnikService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NotRegisteredUserException : Exception
    {
        public NotRegisteredUserException()
        {
        }

        public NotRegisteredUserException(string message) : base(message)
        {
        }

        public NotRegisteredUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotRegisteredUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}