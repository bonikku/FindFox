using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
  public class RegisterModel
  {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
  }
}