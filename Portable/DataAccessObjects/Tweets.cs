using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterMonkey.Models;
using TwitterMonkey.Services.Http;
using TwitterMonkey.Models.Converters;

namespace TwitterMonkey.DataAccessObjects {
  public class Tweets {
    public async static Task<List<Tweet>> Get() {
      var client = new Client();
      var json   = await client.GetAsync("http://goo.gl/pJwOUS");
      return TweetConverter.ConvertAll(json);
    }
  }
}

