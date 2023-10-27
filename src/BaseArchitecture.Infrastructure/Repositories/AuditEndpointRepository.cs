﻿using BaseArchitecture.Application.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BaseArchitecture.Infrastructure.Repositories
{
    public class AuditEndpointRepository : IAuditEndpointRepository
    {
        private readonly IConfiguration _configuration;
        public AuditEndpointRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        } 
        public async Task<int> DeleteOlds(CancellationToken cancellationToken = default)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DEV_STANDAR"));
            await con.OpenAsync(cancellationToken);
            using var cmd = con.CreateCommand();
            cmd.CommandText = "[Sgr].[USP_Delete_Olds_AuditEndpoint]";
            cmd.CommandType = CommandType.StoredProcedure;
            var rowAffect = await cmd.ExecuteNonQueryAsync(cancellationToken);           
            await con.CloseAsync();
            return rowAffect; 
        }
    }
}
