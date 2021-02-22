using System.Text;

namespace OnlineFood.Common.Utility
{
    /// <summary>
    ///     Helper class to generate the random text.
    /// </summary>
    public static class RandomText
    {
        #region Method

        /// <summary>
        ///     Generates the random text.
        /// </summary>
        /// <param name="chars">The specified characters.</param>
        /// <param name="count">The number of characters.</param>
        /// <returns>The random text</returns>
        public static string Generate(string chars, int count)
        {
            var lenght = RandomNumber.Next(count, count);
            var output = new StringBuilder(lenght);

            for (var i = 0; i < lenght; i++)
            {
                var randomIndex = RandomNumber.Next(chars.Length - 1);
                output.Append(chars[randomIndex]);
            }

            return output.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateContractNumber()
        {
            return RandomNumber.Next(1234567).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateTrackingCode()
        {
            return /*string.Format("MM{0}", RandomNumber.Next(1234567));*/ Generate("123456789", 6);
        }
        #endregion
    }
}