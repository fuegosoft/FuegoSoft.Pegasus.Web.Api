using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using FuegoSoft.Pegasus.Lib.Core.Commands;
using System.Data.SqlClient;

namespace FuegoSoft.Pegasus.Lib.Core.Helpers
{
    public static class StringHelper
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string Implode<T>(this IList<T> pieces, string glue)
        {
            string s = "";
            for (int i = 0; i < pieces.Count; i++)
            {
                if (i > 0)
                {
                    s += glue;
                }
                s += pieces[i].ToString();
            }
            return s;
        }

        public static bool IsValidEmail(string email)
        {
            var isEmail = false;

            if (string.IsNullOrEmpty(email))
            {
                string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(email);
                if (match.Success)
                {
                    isEmail = true;
                }
            }

            return isEmail;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static string Base64Encode(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string Base64Decode(string base64Encode)
        {
            var base64EncodingBytes = Convert.FromBase64String(base64Encode);
            return Encoding.UTF8.GetString(base64EncodingBytes);
        }

        public static string GenerateRandomStrings(int length)
        {
            string result = "";
            if (length > 0)
            {
                bool isNotExist = false;
                while (!isNotExist)
                {
                    var generatedString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
                    var checkExtractKeyAlreadyExist = @"
                    SELECT
                        1
                    FROM ContractLinkExpiration
                    WHERE ExtractKey = @ExtractKey";
                    using (var rsCheckExtractKeyAlreadyExist = DbCommand.ExecuteReader(checkExtractKeyAlreadyExist,
                                                                                      new SqlParameter("@ExtractKey", generatedString)))
                    {
                        if (!rsCheckExtractKeyAlreadyExist.Read())
                        {
                            isNotExist = true;
                            result = generatedString;
                        }
                    }
                }
            }
            return result;
        }

        public static string GenerateRandom16Strings()
        {
            return GenerateRandomStrings(16);
        }
    }
}
