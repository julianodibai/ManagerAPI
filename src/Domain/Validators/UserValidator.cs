using FluentValidation;
using Domain.Entities;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {

        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade nao pode ser vazia")

                .NotNull()
                .WithMessage("A entidade nao pode ser nula");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome nao pode ser vazia")

                .NotNull()
                .WithMessage("O nome nao pode ser nulo")

                .MinimumLength(3)
                .WithMessage("O nome deve ter no minimo 3 caracteres")
                
                .MaximumLength(80)
                .WithMessage("O nome deve ter no maximo 80 caracteres");
        
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha nao pode ser vazia")

                .NotNull()
                .WithMessage("A senha nao pode ser nula")

                .MinimumLength(6)
                .WithMessage("A senha deve ter no minimo 6 caracteres")
                
                .MaximumLength(80)
                .WithMessage("A senha deve ter no maximo 80 caracteres");
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email nao pode ser vazio")

                .NotNull()
                .WithMessage("O email nao pode ser nulo")

                .MinimumLength(10)
                .WithMessage("O email deve ter no minimo 10 caracteres")
                .MaximumLength(180)
                .WithMessage("O email deve ter no maximo 180 caracteres")
                
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("O email informado não é válido.");
        }
    }
}