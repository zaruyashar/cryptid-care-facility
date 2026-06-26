using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRYPTIDCARE.Models
{
    public static class Context
    {
        public static string connectionstring = @"Server = localhost; Database = CRYPTIDCARE; Trusted_Connection = True; TrustServerCertificate = True;";

        public static async Task ExecuteReturnAsync(string procname, DynamicParameters param = null)
        {
            using (SqlConnection db = new SqlConnection(connectionstring))
            {
                await db.OpenAsync();
                await db.ExecuteAsync(procname, param, commandType: CommandType.StoredProcedure);
            }
        }

        public static async Task<IEnumerable<T>> ListingAsync<T>(string procname, DynamicParameters param = null)
        {
            using (SqlConnection db = new SqlConnection(connectionstring))
            {
                await db.OpenAsync();
                return await db.QueryAsync<T>(procname, param, commandType: CommandType.StoredProcedure);
            }
        }

        public static async Task<T> GetByIdAsync<T>(string procname, DynamicParameters param = null)
        {
            using (SqlConnection db = new SqlConnection(connectionstring))
            {
                await db.OpenAsync();
                return await db.QueryFirstOrDefaultAsync<T>(procname, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}