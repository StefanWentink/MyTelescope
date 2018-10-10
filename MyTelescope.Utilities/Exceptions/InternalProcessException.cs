namespace MyTelescope.Utilities.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InternalProcessException : Exception
    {
        [Obsolete("Only for serialization")]
        public InternalProcessException()
        {
        }

        [Obsolete("Only for serialization")]
        protected InternalProcessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalProcessException"></see> class
        /// </summary>
        /// <param name="message">The message that is provided with this exception</param>
        public InternalProcessException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalProcessException"></see> class
        /// </summary>
        /// <param name="message">The message that is provided with this exception</param>
        /// <param name="cause">The inner exception that caused this exception</param>
        public InternalProcessException(string message, Exception cause)
            : base(string.IsNullOrEmpty(message) ? "An error ocurred." : message, cause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalProcessException"></see> class
        /// </summary>
        /// <param name="cause">The inner exception that caused this exception</param>
        public InternalProcessException(Exception cause)
            : this(string.Empty, cause)
        {
        }
    }
}
