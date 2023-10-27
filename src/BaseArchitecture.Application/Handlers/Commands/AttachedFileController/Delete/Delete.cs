using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reec.Inspection;

namespace BaseArchitecture.Application.Handlers.Commands.AttachedFileController.Delete
{
    public class Delete : IRequest<ReecMessage>
    {
        public Guid IdAttachedFile { get; }
        public Delete(Guid idAttachedFile)
        {
            IdAttachedFile = idAttachedFile;
        }

        public class DeleteHandler : IRequestHandler<Delete, ReecMessage>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ReecMessage> Handle(Delete request, CancellationToken cancellationToken)
            {
                var entity = await _unitOfWork.AttachedFiles.AsNoTracking()
                                    .Where(af => af.IdAttachedFile == request.IdAttachedFile)
                                    .FirstOrDefaultAsync(cancellationToken);
                if (entity == null)
                    throw new ReecException(ReecEnums.Category.BusinessLogic, ConstantMessage.NotExists);

                entity.RecordStatus = ConstantBase.Inactive;
                _unitOfWork.AttachedFiles.Update(entity);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var message = new ReecMessage(ReecEnums.Category.OK, ConstantMessage.DeleteMessage);
                return message;
            }
        }
    }
}
