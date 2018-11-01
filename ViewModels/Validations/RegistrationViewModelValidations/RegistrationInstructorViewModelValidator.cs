using FluentValidation;

namespace EACA_API.ViewModels.Validations
{
    public class RegistrationInstructorViewModelValidator : AbstractValidator<RegistrationInstructorViewModel>
    {
        public RegistrationInstructorViewModelValidator()
        {
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("Почта не может быть пустой");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Пароль не может быть пустым");
            RuleFor(vm => vm.FirstName).NotEmpty().WithMessage("Имя не может быть пустым");
            RuleFor(vm => vm.LastName).NotEmpty().WithMessage("Фамилия не может быть пустой");
            RuleFor(vm => vm.PhoneNumber).NotEmpty().WithMessage("Номер телефона не может быть пустым");
        }
    }
}
