using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueIrisBridge.Models
{
  public class BaseModel
  {
    public string cmd { get; set; }
    public string session { get; set; }
    public string ApiKey { get; set; }
  }
}
