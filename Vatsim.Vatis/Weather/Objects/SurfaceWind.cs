using System.Collections.Generic;
using System.Runtime.Serialization;
using Vatsim.Vatis.Weather.Enums;
using Vatsim.Vatis.Weather.Objects.Supplements;

namespace Vatsim.Vatis.Weather.Objects
{
    /// <summary>
    /// Surface wind information
    /// </summary>
    [DataContract(Name = "surfaceWind")]
    public class SurfaceWind
    {
        /// <summary>
        /// The raw string value
        /// </summary>
        public string RawValue { get; init; }

        /// <summary>
        /// Current wind direction (heading)
        /// </summary>
        [DataMember(Name = "direction", EmitDefaultValue = false)]
        public int Direction { get; init; }

        /// <summary>
        /// Sign if wind has variable direction (VRB)
        /// </summary>
        [DataMember(Name = "isVariable", EmitDefaultValue = false)]
        public bool IsVariable { get; init; }

        /// <summary>
        /// Speed of the wind
        /// </summary>
        [DataMember(Name = "speed", EmitDefaultValue = false)]
        public int Speed { get; init; }

        /// <summary>
        /// Max wind speed or gust speed
        /// </summary>
        [DataMember(Name = "gustSpeed", EmitDefaultValue = false)]
        public int GustSpeed { get; init; }

        /// <summary>
        /// Type of wind unit
        /// </summary>
        [DataMember(Name = "windUnit", EmitDefaultValue = false)]
        public WindUnit WindUnit { get; init; }

        /// <summary>
        /// Info about extreme wind directions
        /// </summary>
        [DataMember(Name = "extremeWindDirections", EmitDefaultValue = false)]
        public ExtremeWindDirections ExtremeWindDirections { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public SurfaceWind() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">List of parse errors</param>
        internal SurfaceWind(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Wind tokens were not found");
                return;
            }

            RawValue = string.Join(" ", tokens);

            var windValue = tokens[0].Substring(0, 3);
            if (windValue.Equals("VRB"))
                IsVariable = true;
            else
            {
                IsVariable = false;
                Direction = int.Parse(windValue);
            }

            Speed = int.Parse(tokens[0].Substring(3, 2));

            if (tokens[0].Substring(5, 1).Equals("G"))
                GustSpeed = int.Parse(tokens[0].Substring(6, 2));

            WindUnit = GetWindUnit(tokens[0]);

            if (tokens.Length > 1) 
                ExtremeWindDirections = GetExtremeWindDirections(tokens[1]);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get wind unit from the token
        /// </summary>
        /// <param name="unitString"></param>
        /// <returns></returns>
        private WindUnit GetWindUnit(string unitString)
        {
            if (unitString.EndsWith("KMT"))
                return WindUnit.KilometersPerHour;
            if (unitString.EndsWith("KT"))
                return WindUnit.Knots;

            return WindUnit.MetersPerSecond;
        }

        /// <summary>
        /// Parse extreme wind directions token
        /// </summary>
        /// <param name="intervalToken"></param>
        /// <returns></returns>
        private ExtremeWindDirections GetExtremeWindDirections(string intervalToken)
        {
            var directions = intervalToken.Split("V");
            return new ExtremeWindDirections
            {
                FirstExtremeDirection = int.Parse(directions[0]),
                LastExtremeWindDirection = int.Parse(directions[1])
            };
        }

        public int ToKts(int speed)
        {
            return WindUnit switch
            {
                WindUnit.Knots => speed,
                WindUnit.KilometersPerHour => (int)(speed * 0.539957),
                WindUnit.MetersPerSecond => (int)(speed * 1.94384),
                _ => speed,
            };
        }

        public int ToKph(int speed)
        {
            return WindUnit switch
            {
                WindUnit.Knots => (int)(speed * 1.852),
                WindUnit.KilometersPerHour => speed,
                WindUnit.MetersPerSecond => (int)(speed * 3.6),
                _ => speed,
            };
        }

        public int ToMps(int speed)
        {
            return WindUnit switch
            {
                WindUnit.Knots => (int)(speed * 0.514444),
                WindUnit.KilometersPerHour => (int)(speed * 0.277778),
                WindUnit.MetersPerSecond => speed,
                _ => speed,
            };
        }

        #endregion
    }
}
