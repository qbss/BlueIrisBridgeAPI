using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueIrisBridge.Infrastructure
{
  public class AppSettings
  {
    public string BlueIrisBaseUrl { get; set; }
    public string ApiKey { get; set; }
    public string BIUser { get; set; }
    public string BIPassword { get; set; }
  }
}
