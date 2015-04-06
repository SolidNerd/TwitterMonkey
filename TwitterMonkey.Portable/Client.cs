using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;

namespace TwitterMonkey.Portable.Net {
  public class Client {
    public async Task<string> GetResultAsync(string uri){
      var client        = new HttpClient();
      var streamReader  = new StreamReader (await client.GetStreamAsync(uri));
      return await streamReader.ReadToEndAsync();
    }
  }
}

