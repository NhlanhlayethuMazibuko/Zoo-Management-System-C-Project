using System;

namespace PA1.BL
{
    public class InvalidFeatherColorException : Exception
    {
        public InvalidFeatherColorException() : base("Invalid feather color.")
        {
        }

        public InvalidFeatherColorException(string message) : base(message)
        {
        }

        public InvalidFeatherColorException(string message, Exception inner) : base(message, inner)
        {
        }
    } 
}
