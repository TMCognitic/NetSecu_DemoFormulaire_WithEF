using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NetSecu_DemoFormulaire.WebApp.Models.Forms
{
#nullable disable
    public class LoginForm
    {
        [DisplayName("Email :")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Mot de passe :")]
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-=]).{8,20}$")]
        [DataType(DataType.Password)]
        public string Passwd { get; set; }
    }
}
