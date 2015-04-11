using System;

namespace TwitterMonkey.Portable.Utils {
  public abstract class Preferences {
    public abstract string GetString(string key, string defaultValue);
    public abstract bool SaveString(string key, string value);
  }
}

