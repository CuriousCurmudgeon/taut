using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut
{
    public class BaseResponse
    {
        public bool Ok { get; set; }

        /// <summary>
        /// Information about the error if <see cref="Ok"/> is false
        /// </summary>
        public string Error { get; set; }
    }
}
