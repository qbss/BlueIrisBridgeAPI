using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueIrisBridge.Infrastructure
{
  public interface IApiKeyAuthorization
  {
    bool IsValid(string apiKey);
  }
}
