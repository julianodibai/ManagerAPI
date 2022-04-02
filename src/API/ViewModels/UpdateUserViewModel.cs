
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = "O id nao pode ser vazio.")]
        [Range(1, int.MaxValue, ErrorMessage = "O id não pode ser menor que 1.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome nao pode ser vazia.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no minimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no maximo 80 caracteres")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "O nome nao pode ser vazia.")]  
        [MinLength(10, ErrorMessage = "O email nao pode ser vazio")]
        [MaxLength(180, ErrorMessage = "O email deve ter no maximo 180 caracteres")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", 
                            ErrorMessage = "O email informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha nao pode ser vazia")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        [MaxLength(80, ErrorMessage = "A senha deve ter no maximo 80 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", 
                            ErrorMessage = " deve conter ao menos um dígito, deve conter ao menos uma letra minúscula, deve conter ao menos uma letra maiúscula" )]
        public string Password { get; set; }
    }
}