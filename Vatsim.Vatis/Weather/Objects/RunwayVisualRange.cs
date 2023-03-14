using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Weather.Enums;
using Vatsim.Vatis.Weather.Extensions;

namespace Vatsim.Vatis.Weather.Objects
{
    /// <summary>
    /// Info about visibility on the runway (RVR)
    /// </summary>
    [DataContract(Name = "runwayVisualRange")]
    public class RunwayVisualRange
    {
        /// <summary>
        /// The raw RVR string value
        /// </summary>
        [DataMember(Name = "rawValue", EmitDefaultValue = false)]
        public string RawValue { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public RunwayVisualRange() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">List of parse errors</param>
        internal RunwayVisualRange(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array of runway visual range tokens is empty");
                return;
            }

            RawValue = string.Join("", tokens);
        }

        #endregion
    }
}
