using First_App.Server.Models;

namespace First_App.Server.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board> GetBoardByName(string name);

        Task<Board> GetBoardById(Guid id);

        Task<Board> AddBoard(Board board);

        Task<List<Board>> GetAllBoards();

        Task<Board> EditBoard(Guid id, string newName);

        Task DeleteBoard(Guid id);
    }
}
