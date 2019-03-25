using System;
using System.Data.SqlClient;

namespace FuegoSoft.Pegasus.Lib.Core.Helpers
{
    public class ConvertHelper
    {
        /// <summary>
        /// Returns a string from a data reader
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetString(SqlDataReader rs, int index)
        {
            return GetString(rs, index, null);
        }

        /// <summary>
        /// Returns a string from a data reader
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetString(SqlDataReader rs, int index, string def)
        {
            return rs.IsDBNull(index) ? def : rs.GetString(index);
        }

        /// <summary>
        /// Get the value of an index in sqldatareader with the default value of (NULL) if field is DBNULL and return as string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetString(SqlDataReader reader, int index, string check, string def)
        {
            bool condition = GetString(reader, index, check) == check;
            return condition ? def : reader.GetString(index);
        }

        /// <summary>
        /// Returns a boolean from a data reader
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetBoolean(SqlDataReader rs, int index)
        {
            return rs.IsDBNull(index) ? false : rs.GetBoolean(index);
        }

        /// <summary>
        /// Returns an int from a data reader
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static double GetDouble(SqlDataReader reader, int index)
        {
            return GetDouble(reader, index, 0);
        }

        /// <summary>
        /// Returns an int from a data reader with default value.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(SqlDataReader reader, int index, double defaultValue)
        {
            return reader.IsDBNull(index) ? defaultValue : reader.GetDouble(index);
        }

        /// <summary>
        /// Returns a float from a data reader with default value.
        /// </summary>
        /// <returns>The float.</returns>
        /// <param name="rs">Rs.</param>
        /// <param name="index">Index.</param>
        /// <param name="defaultValue">Default value.</param>
        public static decimal GetDecimal(SqlDataReader rs, int index, decimal defaultValue)
        {
            return rs.IsDBNull(index) ? defaultValue : rs.GetDecimal(index);
        }

        /// <summary>
        /// Returns a float from a data reader.
        /// </summary>
        /// <returns>The float.</returns>
        /// <param name="rs">Rs.</param>
        /// <param name="index">Index.</param>
        public static decimal GetDecimal(SqlDataReader rs, int index)
        {
            return GetDecimal(rs, index, 0);
        }

        /// <summary>
        /// Returns a datetime from a data reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(SqlDataReader reader, int index)
        {
            return GetDateTime(reader, index, DateTime.MaxValue);
        }

        /// <summary>
        /// Returns a datetime from a data reader with default value.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(SqlDataReader reader, int index, DateTime defaultValue)
        {
            return reader.IsDBNull(index) ? defaultValue : reader.GetDateTime(index);
        }

        /// <summary>
        /// Returns a GUID field in the SQL data reader
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Guid GetGuid(SqlDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? new Guid() : reader.GetGuid(index);
        }

        /// <summary>
        /// Returns a GUID field in the SQL data reader with default value.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid? GetGuid(SqlDataReader reader, int index, Guid defaultValue)
        {
            return reader.IsDBNull(index) ? defaultValue : reader.GetGuid(index);
        }

        /// <summary>
        /// Get the value of an index in sqldatareader and return as integer
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int GetInt32(SqlDataReader reader, int index)
        {
            return GetInt32(reader, index, 0);
        }

        /// <summary>
        /// Get the value of an index in sqldatareader with the default value of (0) if field is DBNULL and return as integer
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt32(SqlDataReader reader, int index, int defaultValue)
        {
            return reader.IsDBNull(index) ? defaultValue : reader.GetInt32(index);
        }
    }
}
