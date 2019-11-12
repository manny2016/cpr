

namespace Org.Joey.Common
{
    using Microsoft.Extensions.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public static class DatabaseFactory
    {
        public static IDbConnection GenerateDatabase(string connectString = null)
        {
            TryGetConnectionString(out connectString);
            if (string.IsNullOrEmpty(connectString))
                connectString = GenerateMSSqlConnectionString("xx", "x", "x", "xx");
            return new SqlConnection(connectString);
        }
        private static string GenerateMSSqlConnectionString(string server, string database, string userid, string password)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = database,
                UserID = userid,
                Password = password
            };
            return builder.ToString();
        }
        private static bool TryGetConnectionString(out string connectionString)
        {
            connectionString = null;
            try
            {
                 connectionString = IoC.GetService<IConfiguration>().GetConnectionString("ASfP");
                return string.IsNullOrEmpty(connectionString) == false;
            }
            catch
            {
                return false;
            }
        }
    }
}
