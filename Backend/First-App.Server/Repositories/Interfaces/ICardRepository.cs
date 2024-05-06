using First_App.Server.Models;

namespace First_App.Server.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<Priority> GetPriorityById(uint id);

        Task<string> GetPriorityNameById(uint id);

        Task<Card> AddCard(Card card);

        Task<Card> GetCardById(Guid id);

        Task DeleteCard(Guid id);

        Task<Card> EditCard(Card cardToUpdate);
    }
}
