using Core.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
    public class User : Base
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User() //EF
        { }
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            _errors = new List<string>();

            Validate();
        }

        public void ChangeName(string name)
        {
            Name = name;
            Validate();

        }
        public void ChangePassword(string password)
        {
            Password = password;
            Validate();
        }

        public void ChangeEmail(string email)
        {
            Email = email;
            Validate();
        }
        public override bool Validate()
        {
            var validator = new UserValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _errors.Add(error.ErrorMessage);


                throw new DomainException("Alguns campos estão invalidos, por favor corrija-os! ", _errors);
            }

            return true;
        }
    }
}