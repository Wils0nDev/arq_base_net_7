using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Common.Excel;
using MediatR;
using OfficeOpenXml.Table;

namespace BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllByStoreProcedure
{
    public class FindAllByStoreProcedure : IRequest<AttachedFileBase64>
    {

        public class FindAllByStoreProcedureHandler : IRequestHandler<FindAllByStoreProcedure, AttachedFileBase64>
        {
            private readonly IMasterTableRepository _masterTableRepository;
            private readonly IGenerateExcel _generateExcel;

            public FindAllByStoreProcedureHandler(IMasterTableRepository masterTableRepository, IGenerateExcel generateExcel)
            {
                _masterTableRepository = masterTableRepository;
                _generateExcel = generateExcel;
            }

            public async Task<AttachedFileBase64> Handle(FindAllByStoreProcedure request, CancellationToken cancellationToken)
            {
                using var dt = await _masterTableRepository.FindAllByStoreProcedure(cancellationToken);
                var bytes = _generateExcel.SaveToBytes(dt, true, TableStyles.Medium2);
                var fileName = $"FindAllByStoreProcedure.xlsx";

                var file = new AttachedFileBase64()
                {
                    FileBase64String = Convert.ToBase64String(bytes),
                    FileName = fileName
                };
                return file;
            }
        }

    }

}
