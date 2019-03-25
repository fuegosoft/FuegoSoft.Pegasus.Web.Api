using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FuegoSoft.Pegasus.Lib.Core.Commands
{
    public static class DbCommand
    {
        public static IConfiguration Configuration { get; set; }
        private static Object _lock = new Object();

        public static string GetConnectionString(string connectionString)
        {
            string result = "";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            result = Configuration[connectionString];
            return result;
        }


        static int sqlTimeOut() => ((GetConnectionString("ConnectionString:ConnectionTimeOut") == null ? 5 : Convert.ToInt32(GetConnectionString("ConnectionString:ConnectionTimeOut"))) * 60 * 1000);

        /// <summary>
        /// Function that execute query string and return numbers of affected rows.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string query, string connectionString, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(GetConnectionString(connectionString));
            OpenConnection(connection);
            var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            int affectedRows = cmd.ExecuteNonQuery();
            CloseConnection(connection);
            return affectedRows;
        }


        /// <summary>
        /// Function that execute query string and return number of affected rows with default SQL connection.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(query, "ConnectionString:DefaultConnection", parameters);
        }

        /// <summary>
        /// Function that execute query string in a Asynchronous manner and return number of affected rows.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(string query, string connectionString, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(GetConnectionString(connectionString));
            OpenConnection(connection);
            var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            int affectedRows = await cmd.ExecuteNonQueryAsync();
            CloseConnection(connection);
            return affectedRows;
        }

        /// <summary>
        /// Function that execute query string in a Asynchronous manner and return number of affected rows with default SQL connection.
        /// </summary>
        /// <returns>The non query async.</returns>
        /// <param name="query">Query.</param>
        /// <param name="parameters">Parameters.</param>
        public static async Task<int> ExecuteNonQueryAsync(string query, params SqlParameter[] parameters)
        {
            return await ExecuteNonQueryAsync(query, "ConnectionString:DefaultConnection", parameters);
        }

        /// <summary>
        /// Function to read SQL data reader and returns a pointer to the reader being executed by the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string query, string connectionString, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(GetConnectionString(connectionString));
            OpenConnection(connection);
            var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            cmd.CommandTimeout = sqlTimeOut();
            var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// Function to read SQL data reader and returns a pointer to the reader being executed by the query with default SQL connection.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters)
        {
            return ExecuteReader(query, "ConnectionString:DefaultConnection", parameters);
        }

        /// <summary>
        /// Function to read SQL data reader in a Asynchronous manner and returns a pointer to the reader being executed by the query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<SqlDataReader> ExecuteReaderAsync(string query, string connectionString, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(GetConnectionString(connectionString));
            OpenConnection(connection);
            var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            cmd.CommandTimeout = sqlTimeOut();
            var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// Function to read SQL data reader in a Asynchronous manner and returns a pointer to the reader being executed by the query with default SQL connection.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<SqlDataReader> ExecuteReaderAsync(string query, params SqlParameter[] parameters)
        {
            return await ExecuteReaderAsync(query, "ConnectionString:DefaultConnection", parameters);
        }

        /// <summary>
        /// Execute query string and return generic value of type T with default value of T is set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string query, string connectionString, T defaultValue, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(GetConnectionString(connectionString));
            OpenConnection(connection);
            var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            T returnValue = defaultValue;
            var o = cmd.ExecuteScalar();
            if (o != null && o != DBNull.Value)
            {
                returnValue = (T)Convert.ChangeType(o, typeof(T));
            }
            CloseConnection(connection);
            return returnValue;
        }


        /// <summary>
        /// Execute query string and return generic value of type T with default value of T is not set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string query, string connectionString, params SqlParameter[] parameters)
        {
            return ExecuteScalar<T>(query, connectionString, default(T), parameters);
        }

        /// <summary>
        /// Execute query string and return generic value of type T with default SQL Connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string query, params SqlParameter[] parameters)
        {
            return ExecuteScalar<T>(query, "ConnectionString:DefaultConnection", parameters);
        }

        /// <summary>
        /// Execute query string in a Asynchronous manner and return generic value of type T with default value of T is set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsync<T>(string query, string connectionString, T defaultValue, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(GetConnectionString(connectionString));
            OpenConnectionAsync(connection);
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = sqlTimeOut();
                T returnValue = defaultValue;
                var scalar = await cmd.ExecuteScalarAsync();
                if (scalar != null && scalar != DBNull.Value)
                {
                    returnValue = (T)Convert.ChangeType(scalar, typeof(T));
                }
                CloseConnection(connection);
                return returnValue;
            }
        }

        /// <summary>
        /// Execute query string in a Asynchronous manner and return generic value of type T with default value of T is not set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsync<T>(string query, string connectionString, params SqlParameter[] parameters)
        {
            return await ExecuteScalarAsync<T>(query, connectionString, default(T), parameters);
        }

        /// <summary>
        /// Execute query string in a Asynchronous manner and return generic value of type T with default SQL Connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsync<T>(string query, params SqlParameter[] parameters)
        {
            return await ExecuteScalarAsync<T>(query, "ConnectionString:DefaultConnection", parameters);
        }

        /// <summary>
        /// Function to open the connection for use.  Ensures first that the connection is closed before opening.
        /// </summary>
        /// <param name="connection"></param>
        static void OpenConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Function to open the connection in a Asynchronous manner for use.  Ensures first that the connection is closed before opening.
        /// </summary>
        /// <param name="connection"></param>
        static async void OpenConnectionAsync(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
        }

        /// <summary>
        /// Function that closes the SQL connection.  Close only a connection that is open.
        /// </summary>
        /// <param name="connection"></param>
        static void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }


        public static IList<string> ToSqlParameterNames<T>(this List<T> values)
        {
            var names = new List<string>();
            for (int i = 0; i < values.Count; i++)
            {
                names.Add("@Param" + i.ToString());
            }
            return names;
        }

        public static IList<SqlParameter> ToSqlParameters<T>(this List<T> values)
        {
            var parameters = new List<SqlParameter>();
            for (int i = 0; i < values.Count; i++)
            {
                parameters.Add(new SqlParameter("@param" + i.ToString(), values[i]));
            }
            return parameters;
        }

        /// <summary>
        /// Hash and encrypt password to MD5 Algorithm.  Note: This is a one-way hashing.
        /// </summary>
        /// <returns>The password to MD.</returns>
        /// <param name="password">Password.</param>
        [Obsolete("Not recommended, use new RNG algorithm in SecurePasswordHelper class.")]
        public static string HashPasswordToMD5(string password)
        {
            string hashedPassword = "";
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashByteArray = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Feb022018IyongHelpr" + password + "YourHelprAug031992"));
            for (int i = 0; i < hashByteArray.Length; i++)
            {
                hashedPassword += hashByteArray[i];
            }
            return hashedPassword;
        }
    }
}
