using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Interop;
using Squareup.Picasso;
using TwitterMonkey.Adapters;
using TwitterMonkey.DataAccessObjects;
using TwitterMonkey.Utils;


namespace TwitterMonkey {

  [Activity(Label = "TwitterMonkey", MainLauncher = true, Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleTask,Theme = "@style/Theme.AppCompat")]
  public class MainActivity : ActionBarActivity, IGoogleApiClientConnectionCallbacks, IGoogleApiClientOnConnectionFailedListener {

    private const string TAG = "TwitterMonkey";
    private const string HELLOWORLD = "DasistEinzweiterTest";
    private Android.Views.IMenuItem refreshItem;
    private IGoogleApiClient googleApiClient;

    bool isResolvingError = false;

    protected override void OnCreate (Bundle bundle) {
      base.OnCreate(bundle);
      // Set our view from the "main" layout resource
      SupportRequestWindowFeature(WindowCompat.FeatureActionBar);
      SetContentView(Resource.Layout.Main);
      initView();

      googleApiClient = new GoogleApiClientBuilder (this)
        .AddApi(WearableClass.Api)
        .AddConnectionCallbacks(this)
        .AddOnConnectionFailedListener(this)
        .Build();
    }

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
          break;
        case Resource.Id.menu_action_send:
          sendMessageToWearAsync();
          break;
      }
      return true;
    }

    protected override void OnStart () {
      base.OnStart();
        googleApiClient.Connect();
    }


    private async void initView() {
      var listView      = FindViewById<ListView>(Resource.Id.listView1); 
      listView.Adapter  = new TwitterAdapter(this, await Tweets.Get());
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

    private async void sendMessageToWearAsync(){
      await Task.Run(sendMessageToWear);
    }

    private void sendMessageToWear() {
      var fetchedNodes = fetchAllNodes();
      foreach (string node in fetchedNodes) {
        Log.Debug(TAG, node);
        WearableClass.MessageApi.SendMessage(googleApiClient, node, HELLOWORLD, new byte[0]).SetResultCallback((
          new ResultCallback() {
            OnResultAction = (Java.Lang.Object result) => {
              var res = result.JavaCast<IMessageApiSendMessageResult>();
              if (!res.Status.IsSuccess) {
                Log.Error(TAG, "Failed to send message with status code: " + res.Status.StatusCode);
              }
            }
          }
        ));
      }
    }

    private ICollection<string> fetchAllNodes(){
      HashSet<string> results = new HashSet<string> ();
      INodeApiGetConnectedNodesResult nodes = WearableClass.NodeApi.GetConnectedNodes(googleApiClient).Await().JavaCast<INodeApiGetConnectedNodesResult>();
      foreach (var node in nodes.Nodes) {
        results.Add(node.Id);
      }
      return results;
    }


    public void OnConnected (Bundle connectionHint) {
      LOGD (TAG, "Google API CLient was connected");
    }

    public void OnConnectionSuspended (int cause) {
      LOGD (TAG, "Connection to Google API client was suspended");
    }

    public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result) {
      Log.Error(TAG, "Connection to Google API client has failed");
    }

    static void LOGD(string tag, string message) {
        Log.Debug(tag, message);
    }
  }
}


