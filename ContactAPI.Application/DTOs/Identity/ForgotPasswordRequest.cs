using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}