using System;
using Newtonsoft.Json;

namespace TwitterMonkey.Models {
  public class User {
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("profile_image_url")]
    public string ProfileImageUrl { get; set; }

    public override string ToString () {
      return string.Format ("[User: Name={0}, ProfileImageUrl={1}]", Name, ProfileImageUrl);
    }
  }
}

