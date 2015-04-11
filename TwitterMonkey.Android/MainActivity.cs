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
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Views;

using TwitterMonkey.Portable.Utils;

namespace TwitterMonkey {
  [Activity(Label = "TwitterMonkey", MainLauncher = true, Icon = "@drawable/icon",Theme = "@style/Theme.AppCompat")]
  public class MainActivity : ActionBarActivity {

    private const string TAG = "TwitterMonkey";
    private TwitterAdapter twitterAdapter;
    private Preferences preferences;
    public override bool OnCreateOptionsMenu (Android.Views.IMenu menu) {
      // Inflate the menu items for use in the action bar

      MenuInflater.Inflate(Resource.Menu.activity_actions, menu);
      return base.OnCreateOptionsMenu(menu);
    }

    public override  bool OnOptionsItemSelected (Android.Views.IMenuItem item) {
      updateTweets();
      return true;
    }

    protected override void OnCreate (Bundle bundle) {
      base.OnCreate(bundle);
      // Set our view from the "main" layout resource
      SupportRequestWindowFeature(WindowCompat.FeatureActionBar);
      SetContentView(Resource.Layout.Main);
      preferences = new AndroidPreferences(GetSharedPreferences("TwitterMonkey", Android.Content.FileCreationMode.Private));
      init();
    }

    private async void init() {
      var listView      = FindViewById<ListView>(Resource.Id.listView1); 
      listView.Adapter  = twitterAdapter = new TwitterAdapter(this, await Tweets.Get());
    }

    private async void updateTweets(){
      var tweets = await Tweets.Get();
      Log.Debug(TAG, tweets.ToString());
      twitterAdapter.Update(tweets);
    }
  }
}


