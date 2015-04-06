using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using TwitterMonkey.Portable.Twitter;
using TwitterMonkey.Portable;
using Squareup.Picasso;
using Android.Util;
using System.Collections.Generic;


namespace TwitterMonkey {
  [Activity(Label = "TwitterMonkey", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity {
    private const string TAG = "TwitterMonkey";

    protected override async void OnCreate (Bundle bundle) {
      base.OnCreate(bundle);
      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);
      var listView = FindViewById<ListView>(Resource.Id.listView1); 
      // populate the listview with data
      var jsonString    = await fetchJsonAsync(new Uri(Constants.JSON_URI));
      var tweets        = TweetConverter.ConvertAll(jsonString);
      listView.Adapter  = new TwitterAdapter(this, tweets);
    }

    private async Task<string> fetchJsonAsync (Uri uri) {
      HttpWebRequest request = new HttpWebRequest(uri);
      var resp = await request.GetResponseAsync();
      StreamReader reader = new StreamReader (resp.GetResponseStream());
      return await reader.ReadToEndAsync();
    }
  }
}


