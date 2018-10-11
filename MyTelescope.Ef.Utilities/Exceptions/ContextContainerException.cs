namespace MyTelescope.Ef.Utilities.Exceptions
{
    using MyTelescope.Utilities.Exceptions;
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ContextContainerException : InternalProcessException
    {
        [Obsolete("Only for serialization")]
        public ContextContainerException()
        {
        }

        [Obsolete("Only for serialization")]
        protected ContextContainerException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextContainerException"></see> class
        /// Inherits the <see cref="InternalProcessException"></see>
        /// </summary>
        /// <param name="message">The message that is provided with this exception</param>
        public ContextContainerException(string message)
        : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextContainerException"></see> class
        /// Inherits the <see cref="InternalProcessException"></see>
        /// </summary>
        /// <param name="message">The message that is provided with this exception</param>
        /// <param name="cause">The inner exception that caused this exception</param>
        public ContextContainerException(string message, Exception cause)
        : base(message, cause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextContainerException"></see> class
        /// Inherits the <see cref="InternalProcessException"></see>
        /// </summary>
        /// <param name="cause">The inner exception that caused this exception</param>
        public ContextContainerException(Exception cause)
        : base(string.Empty, cause)
        {
        }
    }
}