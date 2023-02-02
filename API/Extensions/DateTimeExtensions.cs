namespace API.Extensions
{
  public static class DateTimeExtensions
  {
    public static int CalculateAge(this DateOnly DateOfBirth)
    {
      // Temporar solution, inaccurate
      var today = DateOnly.FromDateTime(DateTime.UtcNow);

      var age = today.Year - DateOfBirth.Year;

      if (DateOfBirth > today.AddYears(-age)) age--;

      return age;
    }
  }
}