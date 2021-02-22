using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Common.Extensions
{
    public static class PersianExtension
    {
        public static string GetPersianNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 48; i < 58; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(1728 + i));
            }
            return data;
        }

        public static string GetEnglishNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 1777; i < 1786; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(i) == '۰' ? '0' : Convert.ToChar(i - 1728));
            }
            return data;
        }

        public static string GetEnglishNumber2(this string FarsiNumber)
        {
            string numFarsi = string.Empty;
            string numTemp;

            for (int i = 0; i < FarsiNumber.Length; i++)
            {
                numTemp = FarsiNumber.Substring(i, 1);
                switch (numTemp)
                {
                    case "۰":
                        numFarsi = numFarsi + "0";
                        break;
                    case "۱":
                        numFarsi = numFarsi + "1";
                        break;
                    case "۲":
                        numFarsi = numFarsi + "2";
                        break;
                    case "۳":
                        numFarsi = numFarsi + "3";
                        break;
                    case "۴":
                        numFarsi = numFarsi + "4";
                        break;
                    case "۵":
                        numFarsi = numFarsi + "5";
                        break;
                    case "۶":
                        numFarsi = numFarsi + "6";
                        break;
                    case "۷":
                        numFarsi = numFarsi + "7";
                        break;
                    case "۸":
                        numFarsi = numFarsi + "8";
                        break;
                    case "۹":
                        numFarsi = numFarsi + "9";
                        break;
                    default:
                        numFarsi = numFarsi + numTemp;
                        break;
                }
            }
            return (numFarsi);
        }

        public static string GetPersianNumber(this long data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }
        public static string GetPersianNumber(this double data)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0.00}", data).GetPersianNumber();
        }
        public static string GetPersianNumber(this int data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string GetPersianNumber(this decimal data, bool isCurrency = true)
        {
            return isCurrency ? string.Format(CultureInfo.InvariantCulture, "{0:#,###0} تومان", data).GetPersianNumber() : /*string.Format(CultureInfo.InvariantCulture, "{0:#,###0}", data)*/data.ToString("0.#").GetPersianNumber();
            //return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }
        public static string GetPersianNumber(this byte data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }
    }
}

