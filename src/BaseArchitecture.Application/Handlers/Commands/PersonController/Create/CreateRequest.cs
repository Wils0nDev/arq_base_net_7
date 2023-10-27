using BaseArchitecture.Application.Models.Database;

namespace BaseArchitecture.Application.Handlers.Commands.PersonController.Create
{

    public class CreateRequest
    {
        public ModelPerson Person { get; set; }
        public List<ModelAttachedFile> ListAttachedFile { get; set; }

    }

}
