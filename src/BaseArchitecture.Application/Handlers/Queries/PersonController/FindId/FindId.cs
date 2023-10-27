using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models;
using BaseArchitecture.Application.Models.Database;
using BaseArchitecture.Common.Helpers;
using BaseArchitecture.ExternalServices.AwsS3;
using BaseArchitecture.ExternalServices.AwsS3.EndPoint;
using BaseArchitecture.ExternalServices.AwsS3.Models;
using BaseArchitecture.ExternalServices.Happy.EndPoint;
using BaseArchitecture.ExternalServices.Happy.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reec.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Queries.PersonController.FindId
{
    public class FindId : IRequest<FindIdResponse>
    {


        public Guid IdPerson { get; }
        public string Code { get; }
        public string Header { get; }
        public FindId(Guid idPerson, string code, string header)
        {
            IdPerson = idPerson;
            Code = code;
            Header = header;

        }

        public class FindIdHandler : IRequestHandler<FindId, FindIdResponse>
        {
            private readonly IAws _aws;
            private readonly AwsS3AntaminaOptions _awsS3AntaminaOptions;
            private readonly IAuthentication _authentication;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ISecurityServices _security;

            public FindIdHandler(IUnitOfWork unitOfWork, IMapper mapper,
                IAuthentication authentication,
                IAws aws,
                AwsS3AntaminaOptions awsS3AntaminaOptions,
                ISecurityServices security
                )
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _authentication = authentication;
                this._aws = aws;
                this._awsS3AntaminaOptions = awsS3AntaminaOptions;
                _security = security;
            }

            public async Task<FindIdResponse> Handle(FindId request, CancellationToken cancellationToken)
            {

                var entity = await _unitOfWork.Persons.AsNoTracking()
                                .FirstOrDefaultAsync(x => x.IdPerson == request.IdPerson && x.RecordStatus == ConstantBase.Active);

                if (entity == null)
                    throw new ReecException(ReecEnums.Category.BusinessLogic, ConstantMessage.NotExists);

                var attachedFiles = await (
                                        from a in _unitOfWork.PersonFiles.AsQueryable()
                                        join b in _unitOfWork.AttachedFiles.AsQueryable()
                                            on a.IdAttachedFile equals b.IdAttachedFile
                                        where a.IdPerson == entity.IdPerson
                                                && b.RecordStatus == ConstantBase.Active
                                        select b)
                                        .ToListAsync(cancellationToken);

                var header_response = await _authentication.GetDeserializeObject(
                            new EncryptRequest { Code = request.Code, TextTransform = request.Header });

                var aws_credentials = await _security.GetCredentialsByCode(request.Code, request.Header);

                var rf = JsonConvert.DeserializeObject<TokenAws>(header_response.Result.Value);
                
                foreach (var f in attachedFiles)
                {
                    var listPathfile = new List<string>
                    {
                        f.PathFile
                    };

                    var entityAws = new S3PresignedRequest()
                    {
                        bucket = _awsS3AntaminaOptions.AwsBucketName,
                        key = listPathfile,
                        duration = 30
                    };
                    var aws_response = await _aws.S3Presigned(request.Code, request.Header, entityAws);
                    
                    f.PathFile = aws_response.Result.Value.First(x => x.key == f.PathFile).presigned;
                }

                FindIdResponse result = new()
                {
                    Person = _mapper.Map<ModelPerson>(entity),
                    ListAttachedFile = _mapper.Map<List<ModelAttachedFile>>(attachedFiles)
                };


                return result;
            }
        }


    }

}
