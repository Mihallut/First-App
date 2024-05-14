using First_App.Server.Context;
using First_App.Server.Models;
using First_App.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace First_App.Server.Repositories.Classes
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApiDbContext _context;

        public BoardRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Board> AddBoard(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return await GetBoardById(board.Id);
        }

        public async Task DeleteBoard(Guid id)
        {
            var board = await GetBoardById(id);
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }

        public async Task<Board> EditBoard(Guid id, string newName)
        {
            var board = await GetBoardById(id);
            board.Name = newName;
            await _context.SaveChangesAsync();
            return await GetBoardById(id);
        }

        public async Task<List<Board>> GetAllBoards()
        {
            var result = await _context.Boards.ToListAsync();
            var resultOrdered = ApplySorting(result).ToList();
            return resultOrdered;
        }

        private IOrderedEnumerable<Board> ApplySorting(IEnumerable<Board> query)
        {
            return query.OrderBy(b => b.CreationDate);
        }

        public async Task<Board> GetBoardById(Guid id)
        {
            return await _context.Boards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Board> GetBoardByName(string name)
        {
            return await _context.Boards.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
