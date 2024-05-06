using First_App.Server.Models;

namespace First_App.Server.Helpers.Interfases
{
    public interface IActivityLogGenerator
    {
        Task GenerateDeleteLog(Card card);
        Task GenerateCreateLog(Card card);
        Task GenerateEditLog(Card cardBeforeEdit, Card editedCard);
    }
}
