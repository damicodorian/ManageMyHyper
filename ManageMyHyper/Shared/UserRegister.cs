using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMyHyper.Shared
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Veuillez renseigner votre adresse mail."), EmailAddress(ErrorMessage = "Adresse mail au mauvais format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner votre prénom.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner votre nom.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner votre mot de passe."),
            StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Les mots de passe sont différents.")]
        public string ConfirmPassword { get; set; }
        //public UserRole UserRole { get; set; }
        [Required(ErrorMessage = "Veuillez sélectionner votre role.")]
        public int UserRoleId { get; set; }
    }
}
