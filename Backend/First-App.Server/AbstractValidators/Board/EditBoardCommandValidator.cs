using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;

namespace First_App.Server.AbstractValidators.Board
{
    public class EditBoardCommandValidator : AbstractValidator<EditBoardCommand>
    {
        private readonly IBoardRepository _boardRepository;

        public EditBoardCommandValidator(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 60)
                .Matches(@"^[A-Za-z0-9\s-]*$")
                .WithMessage("Board name must be at least 1 character long and no longer than 60 characters. You may use Latin letters only. Digits, special symbols, hyphens and spaces are allowed")
                .Must(BeUniqueName)
                .WithMessage("This board already created.");
        }

        private bool BeUniqueName(string name)
        {
            var board = _boardRepository.GetBoardByName(name);
            return board.Result == null;
        }
    }
}
