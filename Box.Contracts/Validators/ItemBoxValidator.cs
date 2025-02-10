using Box.Contracts.DTO;
using FluentValidation;

namespace Box.Contracts.Validators
{
    /// <summary>
    /// Валидатор для модели <see cref="ShortItemBoxDto"/>.
    /// </summary>
    public class ItemBoxValidator : AbstractValidator<ShortItemBoxDto>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ItemBoxValidator"/>.
        /// </summary>
        public ItemBoxValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Требуется указать имя элемента коробки")
                .Length(2, 200)
                .WithMessage("Имя элемента коробки должно быть от 2 до 200 символов ");

            RuleFor(x => x.Description)
                .Length(2, 500)
                .WithMessage("Описание элемента коробки должно быть от 2 до 500 символов ");
        }
    }
}
