using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace File.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectoryController : ControllerBase
{
    readonly IConfiguration _configuration;

    public DirectoryController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("CreateDirectory")]
    public async Task<IActionResult> CreateDirectoryAsync()
    {
       
    }
}
