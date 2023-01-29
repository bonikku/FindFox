using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
  public class BuggyController : BaseApiController
  {
    private readonly DataContext _context;

    public BuggyController(DataContext context)
    {
      _context = context;
    }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
      return "secret text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
      var obj = _context.Users.Find(-1);

      if (obj == null) return NotFound();

      return obj;
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
      var obj = _context.Users.Find(-1);

      var objToReturn = obj.ToString();

      return objToReturn;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
      return BadRequest("Error 404 not found");
    }
  }
}