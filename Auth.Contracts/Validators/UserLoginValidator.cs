using Auth.Contracts.DTO;
using FluentValidation;

namespace Auth.Contracts.Validators
{
    /// <summary>
    /// Валидатор для проверки данных входа пользователя.
    /// </summary>
    public class UserLoginValidator: AbstractValidator<LoginDto>
    {
        public UserLoginValidator() 
        {
           RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Требуется указать логин")
                .Length(2, 64)
                .WithMessage("Имя пользователя должно быть от 2 до 64 символов ");

           RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Требуется указать имя пароль")
                .MinimumLength(6)
                .WithMessage("Введите пароль, которые состоит больше 6 символов");
        }
    }
    
}
