using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Application.Models.Database;
using BaseArchitecture.Common.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reec.Helpers;
using Reec.Inspection;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllAndDownloadExcel
{
    public class FindAllAndDownloadExcel : IRequest<AttachedFileByte>
    {

        public class FindAllAndDownloadExcelHandler : IRequestHandler<FindAllAndDownloadExcel, AttachedFileByte>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IExcelClosedXml _excelClosedXml;

            public FindAllAndDownloadExcelHandler(IUnitOfWork unitOfWork, IMapper mapper, 
                                                  IExcelClosedXml excelClosedXml)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _excelClosedXml = excelClosedXml;
            }
            public async Task<AttachedFileByte> Handle(FindAllAndDownloadExcel request, CancellationToken cancellationToken)
            {
                var entities = await _unitOfWork.MasterTables.AsNoTracking().ToListAsync();
                var result = _mapper.Map<List<ModelMasterTable>>(entities);
                if (result != null && result.Count == 0)
                    throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

                var bytes = _excelClosedXml.SaveToBytes(entities);
                var fileName = $"{nameof(FindAllAndDownloadExcel)}.xlsx";

                var file = new AttachedFileByte()
                {
                    //ContentType = HelperContentType.GetContentType(fileName),
                    FileByte = bytes,
                    FileName = fileName
                };
                return file;
            }

        }

    }

}
