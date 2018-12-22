using BlueIrisBridge.Infrastructure;
using BlueIrisBridge.Models;
using BlueIrisBridge.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlueIrisBridge.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BlueIrisController : ControllerBase
  {
    private readonly IBlueIrisService _blueIrisService;
    private readonly IApiKeyAuthorization _apiKeyAuthorization;

    public BlueIrisController(IBlueIrisService blueIrisService, IApiKeyAuthorization apiKeyAuthorization)
    {
      _blueIrisService = blueIrisService;
      _apiKeyAuthorization = apiKeyAuthorization;
    }

    [HttpPost("Pause")]
    public async Task<ActionResult<string>> Post([FromBody] PauseCameraModel value)
    {
      if (!_apiKeyAuthorization.IsValid(value.ApiKey)) { return BadRequest(); }

      await _blueIrisService.PauseAsync(value);
      return Ok();
    }
  }
}
