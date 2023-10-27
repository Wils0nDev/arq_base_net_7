using BaseArchitecture.Application.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Infrastructure.Repositories
{
    public class DemoLogRepository : IDemoLogRepository
    {
        private readonly IConfiguration _configuration;
        public DemoLogRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        } 

        public async Task<string> InternalServerError_SP()
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DEV_STANDAR"));
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "Demo.USP_InternalServerError";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            var dt = new DataTable();
            dt.Load(reader);
            await con.CloseAsync();
            return null;
        }

        public async Task<string> InternalServerError_SP_Transaction()
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DEV_STANDAR"));
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "Demo.USP_InternalServerError_Transaction";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            var dt = new DataTable();
            dt.Load(reader);
            await con.CloseAsync();
            return null;
        }
    }
}
