using Android.Gms.Wearable;
using Android.App;
using Android.Gms.Common.Apis;
using Android.Util;

namespace TwitterMonkey.Wear {
  /* These both Attributes will, be compiled
   * to the Android Manifest it replaces the
   * Default way of setting special Stuff
   * in the Android Manifest
   */
  [Service(), IntentFilter(new string[]{"com.google.android.gms.wearable.BIND_LISTENER"})]
  public class DataLayerListenerService : WearableListenerService {

    const string TAG = "DataLayerListenerServic";
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
  }
}

