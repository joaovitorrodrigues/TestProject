using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestProject.Domain.Enums;

namespace TestProject.Web.Models
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }
        [Required]
        public  string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("CNPJ")]
        public long Cnpj { get; set; }
        [Required]
        [DisplayName("Birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [DisplayName("Cnh number")]
        public int CnhNumber { get; set; }
        [Required]
        [DisplayName("CNH")]
        public CnhType CnhType { get; set; }
        [Required]
        [DisplayName("Cnh image")]
        public IFormFile CnhImage { get; set;}
    }
}
