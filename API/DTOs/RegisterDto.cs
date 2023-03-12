using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
  public class RegisterDto
  {
    [Required]
    [StringLength(16, MinimumLength = 4)]
    public string Username { get; set; }
    [Required] public string Aka { get; set; }
    [Required] public string Gender { get; set; }
    [Required] public DateOnly? DateOfBirth { get; set; }
    [Required] public string Forest { get; set; }
    [Required] public string Country { get; set; }
    [Required]
    [StringLength(16, MinimumLength = 4)]
    public string Password { get; set; }
  }
}