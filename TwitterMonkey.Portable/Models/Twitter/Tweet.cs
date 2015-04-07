using Newtonsoft.Json;

namespace TwitterMonkey.Portable.Models.Twitter {
  public class Tweet {
    [JsonProperty("text")]
    public string Message { get; set; }
    [JsonProperty("user")]
    public User User { get; set; }

    public override string ToString () {
      return string.Format ("[Tweet: Message={0}, User={1}]", Message, User);
    }
  }
}

