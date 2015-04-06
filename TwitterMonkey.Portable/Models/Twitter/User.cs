using System;
using Newtonsoft.Json;

namespace TwitterMonkey.Portable.Models.Twitter {
  public class User {
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("profile_image_url")]
    public string ProfileImageUrl { get; set; }
  }
}

