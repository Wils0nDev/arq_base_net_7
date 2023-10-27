using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Application.Models.Database;
using BaseArchitecture.Common.Helpers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BaseArchitecture.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IConfiguration _configuration;
        private readonly HeaderToken _token;

        public PersonRepository(IConfiguration configuration, HeaderToken token)
        {
            _configuration = configuration;
            _token = token;
        }
        public async Task<int> CreateAndUpdateByStoredProcedure(ModelPerson person)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DEV_STANDAR")))
            {

                var parameters = new DynamicParameters();
                parameters.Add("@ParamIdPerson", person.IdPerson);
                parameters.Add("@ParamDNI", person.DNI);
                parameters.Add("@ParamName", person.Name);
                parameters.Add("@ParamLastName", person.LastName);
                parameters.Add("@ParamPhone", person.Phone);
                parameters.Add("@ParamBirthday", person.Birthday);
                parameters.Add("@ParamNationality", person.Nationality);
                parameters.Add("@ParamCity", person.City);
                parameters.Add("@ParamEmployment", person.Employment);
                parameters.Add("@ParamPathFile", person.PathFile);
                parameters.Add("@ParamRecordStatus", person.RecordStatus);
                parameters.Add("@ParamUser", _token.UserEdit);

                return await connection.ExecuteAsync(
                    sql: $"{ConstantMasterTable.Schema.Demo}.{ConstantMasterTable.Procedure.CreateAndUpdatePerson}",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
