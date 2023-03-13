using Vatsim.Vatis.Weather.Enums;
using Vatsim.Vatis.Weather.Objects;
using Vatsim.Vatis.Weather.TokenLogic;

namespace Vatsim.Vatis.Weather
{
    /// <summary>
    /// General METAR parser class
    /// </summary>
    public class MetarParser
    {
        /// <summary>
        /// Current month
        /// </summary>
        private Month _currentMonth;

        public MetarParser(Month currentMonth = Month.None)
        {
            _currentMonth = currentMonth;
        }

        #region Public methods

        /// <summary>
        /// Parse method
        /// </summary>
        /// <param name="metar">Raw METAR string</param>
        /// <returns>Parsed Metar object</returns>
        public Metar Parse(string metar)
        {
            if (string.IsNullOrEmpty(metar))
                return new Metar
                {
                    ParseErrors = new []{ "Raw METAR is not correct" }
                };

            var rawTokens = metar.Replace("=", "").ToUpper().Split(" ");
            var groupedTokens = Recognizer.Instance().RecognizeAndGroupTokens(rawTokens);

            return new Metar(groupedTokens, _currentMonth)
            {
                RawMetar = metar
            };
        }

        /// <summary>
        /// Multiple parse METARs method
        /// </summary>
        /// <param name="raws">Array of raw METAR strings</param>
        /// <returns>Array of parsed Metar objects</returns>
        public Metar[] Parse(string[] raws)
        {
            var metars = new Metar[raws.Length];
            for (var i = 0; i < raws.Length; i++)
            {
                metars[i] = Parse(raws[i]);
            }

            return metars;
        }

        #endregion
    }
}
