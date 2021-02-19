using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    /// <summary>
    /// DakarRally application error.
    /// </summary>
    public class DakarRallyApplicationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DakarRallyApplicationError"/> class.
        /// </summary>
        /// <param name="errorsList">Thelist of errors that occured.</param>
        public DakarRallyApplicationError(List<string> errorsList)
        {
            ErrorsList = errorsList;
        }

        /// <summary>
        /// List of error messages.
        /// </summary>
        public List<string> ErrorsList { get; set; }
    

    }
}

