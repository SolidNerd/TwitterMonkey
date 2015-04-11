using TwitterMonkey.Portable.Utils;
using Android.Content;
using Android.Util;

namespace TwitterMonkey {
  public class AndroidPreferences : Preferences {

    private ISharedPreferences SharedPreferences;

    public AndroidPreferences(){ }

    public AndroidPreferences(ISharedPreferences sharedPreferences){
      SharedPreferences = sharedPreferences;
    }


    public override string GetString (string key, string defaultValue) {
      var preferenceString = SharedPreferences.GetString(key, defaultValue);
      Log.Debug("TwitterMonkey",string.Format("Load Setting String: {0} = {1}",key,preferenceString));
      return preferenceString;
    }
    public override bool SaveString (string key, string value) {
      var editor = SharedPreferences.Edit();
      editor.PutString(key, value);
      return editor.Commit();
    }
    
  }
}

