using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BaseArchitecture.Infrastructure.Repositories
{
    public class MasterTableRepository : IMasterTableRepository
    {
        private readonly IConfiguration _configuration;
        public MasterTableRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<DataTable> FindAllByStoreProcedure(CancellationToken cancellationToken = default)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DEV_STANDAR"));
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "Cnfg.USP_LIST_MasterTableAll";
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            var dt = new DataTable();
            dt.Load(reader);
            await con.CloseAsync();
            return dt;
        }


    }

}
