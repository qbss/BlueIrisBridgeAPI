using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueIrisBridge.Models
{
  public class PauseCameraModel : BaseModel
  {
    public string camera { get; set; }
    public int pause { get; set; }

    public PauseCameraModel()
    {
      cmd = "camconfig";
    }
  }
}
