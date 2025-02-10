using Box.Contracts.DTO;
using FluentValidation;

namespace Box.Contracts.Validators
{
    /// <summary>
    /// Валидатор для модели <see cref="ShortBoxDto"/>.
    /// </summary>
    public class BoxValidator : AbstractValidator<ShortBoxDto>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BoxValidator"/>.
        /// </summary>
        public BoxValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Требуется указать название коробки")
                .Length(2, 100)
                .WithMessage("Имя коробки должно быть от 2 до 100 символов ");
        }
    }
}
