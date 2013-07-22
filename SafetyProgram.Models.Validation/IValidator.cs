using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Models.Validation
{
    /// <summary>
    /// Defines an interface for data validators which validate the state of objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Validate the data.
        /// </summary>
        /// <param name="data">The data to be validated.</param>
        /// <returns>Erroneous data with error messages.</returns>
        IEnumerable<IInvalidDataError> Validate(T data);
    }
}
