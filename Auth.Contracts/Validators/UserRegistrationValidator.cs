using Auth.Contracts.DTO;
using FluentValidation;

namespace Auth.Contracts.Validators
{
    /// <summary>
    /// Валидатор для регистрации пользователя.
    /// </summary>
    public class UserRegistrationValidator: AbstractValidator<RegisterDto>
    {
        public UserRegistrationValidator() 
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Требуется указать имя пользователя")
                .Length(2, 64)
                .WithMessage("Имя пользователя должно быть от 2 до 64 символов ");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Требуется указать имя пароль")
                .MinimumLength(6)
                .WithMessage("Введите пароль, которые состоит больше 6 символов");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Требуется указать ваш почтовый адрес")
                .EmailAddress()
                .WithMessage("Неверный формат электронной почты");
        }
    }
}
