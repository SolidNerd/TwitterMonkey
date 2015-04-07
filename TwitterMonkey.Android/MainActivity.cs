using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using Squareup.Picasso;
using TwitterMonkey.Portable.DataAccessObjects;
using TwitterMonkey.Views;


namespace TwitterMonkey {
  [Activity(Label = "TwitterMonkey", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity {

    private const string TAG = "TwitterMonkey";
    protected override  void OnCreate (Bundle bundle) {
      base.OnCreate(bundle);
      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);
      init();
    }

    private async void init() {
      var listView      = FindViewById<ListView>(Resource.Id.listView1); 
      listView.Adapter  = new TwitterAdapter(this, await Tweets.Get());
    }

  }
}


