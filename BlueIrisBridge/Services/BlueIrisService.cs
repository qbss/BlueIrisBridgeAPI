using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlueIrisBridge.Infrastructure;
using BlueIrisBridge.Models;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlueIrisBridge.Services
{
  public class BlueIrisService : IBlueIrisService
  {
    private static string session;
    private readonly IOptions<AppSettings> _appSettings;
    private readonly ILogger<BlueIrisService> _logger;
    private readonly Url _url;

    public BlueIrisService(IOptions<AppSettings> appSettings, ILogger<BlueIrisService> logger)
    {
      _logger = logger;
      _appSettings = appSettings;
      _url = new Url($"{_appSettings.Value.BlueIrisBaseUrl}/json");
    }

    private async Task LoginAsync()
    {
      var loginResponse = await _url.PostJsonAsync(new { cmd = "login" }).ReceiveJson<LoginResponse>();
      session = loginResponse.Session;

      string response = GetMd5Hash($"{_appSettings.Value.BIUser}:{session}:{_appSettings.Value.BIPassword}");
      loginResponse = await _url.PostJsonAsync(new { cmd = "login", response, session }).ReceiveJson<LoginResponse>();

      session = loginResponse.Session;
    }

    public async Task PauseAsync(PauseCameraModel pauseCameraModel)
    {
      await LoginAsync();
      pauseCameraModel.session = session;

      string camera = pauseCameraModel.camera;
      if (pauseCameraModel.camera.StartsWith("the "))
      {
        camera = pauseCameraModel.camera.Substring(4);
      }

      var tasks = GetPauseAmountsForHours(pauseCameraModel.pause)
        .Select(val => new PauseCameraModel() { ApiKey = pauseCameraModel.ApiKey, pause = val, session = session, camera = camera })
        .Select(model => _url.PostJsonAsync(model)).ToArray();

      Task.WaitAll(tasks);
    }

    private static IEnumerable<int> GetPauseAmountsForHours(int hours)
    {
      switch (hours)
      {
        case 1:
          return new[] { 4 };
        case 2:
          return new[] { 5 };
        case 3:
          return new[] { 6 };
        case 4:
          return new[] { 6, 4 };
        case 5:
          return new[] { 7 };
        case 6:
          return new[] { 4, 7 };
        case 7:
          return new[] { 5, 7 };
        case 8:
          return new[] { 6, 7 };
        case 9:
          return new[] { 7, 6, 4 };
        case 10:
          return new[] { 8 };
        case 11:
          return new[] { 8, 4 };
        case 12:
          return new[] { 8, 5 };
        default:
          return new[] { hours };
      }
    }


    private static string GetMd5Hash(string input)
    {
      using (MD5 md5Hash = MD5.Create())
      {
        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
          sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
      }
    }
  }
}
