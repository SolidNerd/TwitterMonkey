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
using Android.Views.Animations;

namespace TwitterMonkey {
  [Activity(Label = "TwitterMonkey", MainLauncher = true, Icon = "@drawable/icon",Theme = "@style/Theme.AppCompat")]
  public class MainActivity : ActionBarActivity {

    private const string TAG = "TwitterMonkey";
    private Preferences preferences;
    private Android.Views.IMenuItem refreshItem;

    public override bool OnCreateOptionsMenu (Android.Views.IMenu menu) {
      // Inflate the menu items for use in the action bar
      MenuInflater.Inflate(Resource.Menu.activity_actions, menu);
      return base.OnCreateOptionsMenu(menu);
    }

    public override  bool OnOptionsItemSelected (Android.Views.IMenuItem item) {
      switch (item.ItemId) {
        case Resource.Id.menu_action_refresh:
          refreshItem = item;
          refresh();
          preferences.SaveString("access_token", "abc123");
          break;
      }
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
      listView.Adapter  = new TwitterAdapter(this, await Tweets.Get());
      preferences.GetString("access_token", string.Empty);
    }

    private void refresh(){
      var imageView = LayoutInflater.Inflate(Resource.Layout.refresh_action_view, null);
      var rotation  = AnimationUtils.LoadAnimation(this, Resource.Animation.clockwise_refresh);
      rotation.RepeatCount = Animation.Infinite;
      imageView.StartAnimation(rotation);
      refreshItem.SetActionView(imageView);
    }

    private void completeRefresh(){
      refreshItem.ActionView.ClearAnimation();
      refreshItem.SetActionView(null);
    }
  }
}


