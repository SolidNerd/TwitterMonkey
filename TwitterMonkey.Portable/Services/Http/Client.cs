using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;

namespace TwitterMonkey.Portable.Services.Http {
  public class Client {
    public async Task<string> GetAsync(string uri){
      var client    = new HttpClient();
      var response  = await client.GetAsync(uri);
      return await response.Content.ReadAsStringAsync();
    }
  }
}

