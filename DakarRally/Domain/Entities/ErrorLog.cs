namespace DakarRally.Domain.Entities
{
    /// <summary>
    /// Log of the error information.
    /// </summary>
    public class ErrorLog : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLog"/> class.
        /// </summary>
        public ErrorLog()
        {
        }

        /// <summary>
        /// Type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Stack trace.
        /// </summary>
        public string StackTrace { get; set;  }

        /// <summary>
        /// Source.
        /// </summary>
        public virtual string Source { get; set; }

        /// <summary>
        /// Messsage.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// Messsage.
        /// </summary>
        public string InnerException { get; set; }

        /// <summary>
        /// HRESULT.
        /// </summary>
        public int HResult { get; set; }
    }
}
