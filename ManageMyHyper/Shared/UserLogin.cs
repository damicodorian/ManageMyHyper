using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMyHyper.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Veuillez renseigner votre adresse mail.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner votre mot de passe.")]
        public string Password { get; set; }
    }
}
