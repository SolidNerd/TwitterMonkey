
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TwitterMonkey.Wear {
  [IntentFilter( new string[]{ "android.intent.action.MAIN" }, Categories = new string[]{ "android.intent.category.LAUNCHER" })]
  [Activity(Label = "MainActivity", MainLauncher = true, Icon= "@drawable/icon")]      
  public class MainActivity : Activity {
    protected override void OnCreate (Bundle bundle) {
      base.OnCreate(bundle);

      // Create your application here
    }
  }
}

