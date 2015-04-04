using Newtonsoft.Json;

namespace TwitterMonkey.Portable {
  public class Tweet {
    [JsonProperty("text")]
    public string Message { get; set; }
  }
}

