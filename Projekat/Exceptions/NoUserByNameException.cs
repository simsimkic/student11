// File:    KorisnikService.cs
// Created: Saturday, July 4, 2020 12:27:59 PM
// Purpose: Definition of Class KorisnikService

using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class NoUserByNameException : Exception
    {
        public NoUserByNameException()
        {
        }

        public NoUserByNameException(string message) : base(message)
        {
        }

        public NoUserByNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoUserByNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}