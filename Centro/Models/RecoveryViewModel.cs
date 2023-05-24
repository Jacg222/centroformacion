using System.ComponentModel.DataAnnotations;

namespace centro.Models
{
    public class RecoveryViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
