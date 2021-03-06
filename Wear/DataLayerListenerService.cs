﻿using Android.Gms.Wearable;
using Android.App;
using Android.Gms.Common.Apis;
using Android.Util;
using Android.Content;

namespace TwitterMonkey.Wear {
  /* These both Attributes will, be compiled
   * to the Android Manifest it replaces the
   * Default way of setting special Stuff
   * in the Android Manifest
   */
  [Service(), IntentFilter(new string[]{"com.google.android.gms.wearable.BIND_LISTENER"})]
  public class DataLayerListenerService : WearableListenerService {

    const string TAG = "DataLayerListenerServic";
    private const string StartActivityPath = "/start-activity";

    private IGoogleApiClient googleApiClient;

    public override void OnCreate () {
      base.OnCreate();
      googleApiClient = new GoogleApiClientBuilder(this.ApplicationContext)
        .AddApi(WearableClass.Api)
        .Build();
      googleApiClient.Connect();
    }

    public override void OnMessageReceived (IMessageEvent messageEvent) {
      base.OnMessageReceived(messageEvent);
      LOGD(TAG, "OnMessageReceived: " + messageEvent);

      if(messageEvent.Path == StartActivityPath) {
        startNewMainActivity();
      }
    }

    public override void OnPeerConnected (INode peer) {
      LOGD(TAG, "OnPeerConnected: " + peer);
    }

    public override void OnPeerDisconnected (INode peer) {
      LOGD(TAG, "OnPeerDisconnected: " + peer);
    }

    public static void LOGD(string tag, string message) {
        Log.Debug(tag, message);
    }

    private void startNewMainActivity(){
      var intent = new Intent (this, typeof(MainActivity));
      intent.AddFlags(ActivityFlags.NewTask);
      StartActivity(intent);
    }
  }
}

