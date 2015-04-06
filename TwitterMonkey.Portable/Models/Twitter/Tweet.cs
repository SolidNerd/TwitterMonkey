using Newtonsoft.Json;

namespace TwitterMonkey.Portable.Models.Twitter {
  public class Tweet {
    [JsonProperty("text")]
    public string Message { get; set; }
    [JsonProperty("user")]
    public User User { get; set; }
  }
}

