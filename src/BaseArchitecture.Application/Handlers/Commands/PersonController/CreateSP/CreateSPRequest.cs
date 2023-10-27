using BaseArchitecture.Application.Models.Database;

namespace BaseArchitecture.Application.Handlers.Commands.PersonController.CreateSP
{ 

    public class CreateSPRequest
    {
        public ModelPerson Person { get; set; }
        public List<ModelAttachedFile> ListAttachedFile { get; set; }

    }

}
