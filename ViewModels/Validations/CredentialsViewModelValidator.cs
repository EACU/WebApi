using FluentValidation;

namespace EACA_API.ViewModels.Validations
{
    public class CredentialsViewModelValidator : AbstractValidator<CredentialsViewModel>
    {
        public CredentialsViewModelValidator()
        {
            RuleFor(vm => vm.UserName).NotEmpty().WithMessage("Имя пользователя не может быть пустым");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Пароль не может быть пустым");
            RuleFor(vm => vm.Password).Length(6, 12).WithMessage("Пароль должен быть от 6 до 12 символов");
        }
    }
}
