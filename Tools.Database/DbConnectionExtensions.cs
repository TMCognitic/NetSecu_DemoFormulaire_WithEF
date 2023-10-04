using System.Data.Common;
using System.Data;
using System.Reflection;

namespace Tools.Database
{
    public static class DbConnectionExtensions
    {
        public static int ExecuteNonQuery(this DbConnection dbConnection, string query, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                return dbCommand.ExecuteNonQuery();
            }
        }

        public static object? ExecuteScalar(this DbConnection dbConnection, string query, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                object? result = dbCommand.ExecuteScalar();
                return result is DBNull ? null : result;
            }
        }

        public static IEnumerable<TResult> ExecuteReader<TResult>(this DbConnection dbConnection, string query, Func<IDataReader, TResult> mapper, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return mapper(reader);
                    }
                }
            }
        }

        private static void EnsureValidConnection(DbConnection dbConnection)
        {
            ArgumentNullException.ThrowIfNull(dbConnection, nameof(dbConnection));
            if (dbConnection.State is not ConnectionState.Open)
            {
                throw new InvalidOperationException("The connection must be open for execute a Sql Command.");
            }
        }

        private static DbCommand CreateCommand(DbConnection dbConnection, string query, bool isStoredProcedure, object? parameters)
        {
            EnsureValidConnection(dbConnection);

            DbCommand dbCommand = dbConnection.CreateCommand();

            dbCommand.CommandText = query;

            if (isStoredProcedure)
                dbCommand.CommandType = CommandType.StoredProcedure;

            if (parameters is not null)
            {
                Type parametersType = parameters.GetType();

                IEnumerable<PropertyInfo> propertyInfos = parametersType.GetProperties().Where(p => p.CanRead);

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    DbParameter dbParameter = dbCommand.CreateParameter();
                    dbParameter.ParameterName = propertyInfo.Name;
                    dbParameter.Value = propertyInfo.GetMethod?.Invoke(parameters, null) ?? DBNull.Value;
                    dbCommand.Parameters.Add(dbParameter);
                }
            }

            return dbCommand;
        }
    }
}