using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;

namespace First_App.Server.AbstractValidators.Board
{
    public class DeleteBoardCommandValidator : AbstractValidator<DeleteBoardCommand>
    {
        private readonly IBoardRepository _boardRepository;

        public DeleteBoardCommandValidator(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;

            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(ExistInDb)
                .WithMessage("Board with provided guid does not exist in database.");
        }

        private bool ExistInDb(Guid guid)
        {
            var board = _boardRepository.GetBoardById(guid);
            return board.Result != null;
        }
    }
}
