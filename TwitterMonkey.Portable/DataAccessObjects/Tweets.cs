using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterMonkey.Portable.Models.Twitter;
using TwitterMonkey.Portable.Services.Http;

namespace TwitterMonkey.Portable.DataAccessObjects {
  public class Tweets {
    public async static Task<List<Tweet>> Get() {
      var client = new Client();
      var json   = await client.GetAsync("http://goo.gl/pJwOUS");
      return TweetConverter.ConvertAll(json);
    }
  }
}

