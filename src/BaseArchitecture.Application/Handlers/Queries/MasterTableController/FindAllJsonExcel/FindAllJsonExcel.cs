using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Common.Excel;
using BaseArchitecture.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Table;
using Reec.Helpers;
using Reec.Inspection;
using System.Drawing;
using System.Net;
using System.Text;

namespace BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllJsonExcel
{
    public class FindAllJsonExcel : IRequest<AttachedFileBase64>
    {

        public class FindAllJsonExcelHandler : IRequestHandler<FindAllJsonExcel, AttachedFileBase64>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IGenerateExcel _generateExcel;
            private readonly IMasterTableRepository _masterTableRepository;

            public FindAllJsonExcelHandler(IUnitOfWork unitOfWork, IMapper mapper, IGenerateExcel generateExcel, IMasterTableRepository masterTableRepository)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _generateExcel = generateExcel;
                _masterTableRepository = masterTableRepository;
            }
            public async Task<AttachedFileBase64> Handle(FindAllJsonExcel request, CancellationToken cancellationToken)
            {
                var result = await
                (from a in _unitOfWork.MasterTables.AsQueryable()
                 select new
                 {
                     a.IdMasterTable,
                     a.IdMasterTableParent,
                     a.Name,
                     a.Order,
                     a.Value,
                     Status = a.RecordStatus == ConstantBase.Active ? ConstantBase.ActiveDescription : ConstantBase.InactiveDescription,
                     a.UserRecordCreation,
                     RecordCreationDate = a.RecordCreationDate.ToString("dd/MM/yyyy HH:mm"),
                     a.UserEditRecord,
                     RecordEditDate = a.RecordEditDate.HasValue ? a.RecordEditDate.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty
                 }).ToListAsync(cancellationToken);

                if (result != null && result.Count == 0)
                    throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");


                var bytes = _generateExcel.SaveToBytes(result, true, TableStyles.Medium2);
                var fileName = string.Format("Reporte_Master_Table_{0}.xlsx", DateTime.Now.ToString("yyyy_MM_dd_hh_mm"));

                var file = new AttachedFileBase64()
                {
                    //ContentType = HelperContentType.GetContentType(fileName),
                    FileBase64String = Convert.ToBase64String(bytes),
                    FileName = fileName
                };
                return file;
            }
        }

    }

}
