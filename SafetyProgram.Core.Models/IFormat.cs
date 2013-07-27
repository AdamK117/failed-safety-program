using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Models
{
    public interface IFormat
    {
        /// <summary>
        /// Get or set the width of the document
        /// </summary>
        int Width { get; set; }
    }
}
