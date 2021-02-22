using System;
using System.Security.Cryptography;

namespace OnlineFood.Common.Utility
{
    /// <summary>
    ///     Helper class to generates the random numbers.
    /// </summary>
    public static class RandomNumber
    {
        #region Fields

        private static int size = 7;
        private static readonly byte[] Randb = new byte[size];
        private static readonly RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

        #endregion

        #region Methods

        /// <summary>
        ///     Generates a positive random number.
        /// </summary>
        private static int Next()
        {
            Rand.GetBytes(Randb);
            var value = BitConverter.ToInt32(Randb, 0);
            return Math.Abs(value);
        }

        /// <summary>
        ///     Generates a positive random number.
        /// </summary>
        public static int Next(int max)
        {
            return Next() % (max + 1);
        }

        /// <summary>
        ///     Generates a positive random number.
        /// </summary>
        public static int Next(int min, int max)
        {
            return Next(max - min) + min;
        }


        public static string GenerateMobileNumberToken(int size = 7)
        {
            return Next(1001, 9999).ToString();
        }
        #endregion
    }
}