using System.Collections.Generic;
using TwitterMonkey.Portable.Models.Twitter;
using TwitterMonkey.Portable.Net;
using System.Threading.Tasks;

namespace TwitterMonkey.Portable {
  public class Tweets {
    public async static Task<List<Tweet>> Get() {
      var client = new NetHttp ();
      var json   = await client.fetchJsonAsync("http://goo.gl/pJwOUS");
      return TweetConverter.ConvertAll(json);
    }
  }
}

