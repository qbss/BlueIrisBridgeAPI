using BlueIrisBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueIrisBridge.Services
{
  public interface IBlueIrisService
  {
    Task PauseAsync(PauseCameraModel model);
  }
}
