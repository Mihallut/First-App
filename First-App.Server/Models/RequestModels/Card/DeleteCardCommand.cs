using MediatR;

namespace First_App.Server.Models.RequestModels.Card
{
    public class DeleteCardCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
