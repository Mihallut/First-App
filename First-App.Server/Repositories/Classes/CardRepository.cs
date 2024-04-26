using First_App.Server.Context;
using First_App.Server.Models;
using First_App.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace First_App.Server.Repositories.Classes
{
    public class CardRepository : ICardRepository
    {
        private readonly ApiDbContext _context;

        public CardRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Card> AddCard(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return await GetCardById(card.Id);
        }

        public async Task DeleteCard(Guid id)
        {
            var card = await GetCardById(id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<Card> EditCard(Card cardToEdit)
        {
            var card = await GetCardById(cardToEdit.Id);
            card.Description = cardToEdit.Description;
            card.TaskListId = cardToEdit.TaskListId;
            card.DueDate = cardToEdit.DueDate;
            card.PriorityId = cardToEdit.PriorityId;
            await _context.SaveChangesAsync();
            return await GetCardById(cardToEdit.Id);
        }

        public async Task<Card> GetCardById(Guid id)
        {
            var result = await _context.Cards.Include(x => x.Priority).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Priority> GetPriorityById(uint id)
        {
            var result = await _context.Priorities.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<string> GetPriorityNameById(uint id)
        {
            var result = await _context.Priorities.FirstOrDefaultAsync(x => x.Id == id);
            return result.Name;
        }
    }
}
