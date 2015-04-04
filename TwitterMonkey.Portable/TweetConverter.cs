using Newtonsoft.Json;
using System.Collections.Generic;
using TwitterMonkey.Portable.Twitter;

namespace TwitterMonkey.Portable {
  public class TweetConverter {
    public static Tweet Convert(string json){
      return JsonConvert.DeserializeObject<Tweet>(json);
    }
    public static List<Tweet> ConvertAll(string json){
      return JsonConvert.DeserializeObject<List<Tweet>>(json);
    }
  }
}

