using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueIrisBridge.Infrastructure
{
  public class ApiKeyAuthorization : IApiKeyAuthorization
  {
    private readonly IOptions<AppSettings> _appSettings;
    public ApiKeyAuthorization(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings;
    }

    public bool IsValid(string apiKey)
    {
      return string.Equals(apiKey, _appSettings.Value.ApiKey, StringComparison.InvariantCulture);
    }
  }
}
