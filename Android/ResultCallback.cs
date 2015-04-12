using System;
using Android.Gms.Common.Apis;

namespace TwitterMonkey {
  public class ResultCallback : Java.Lang.Object, IResultCallback {
    public Action<Java.Lang.Object> OnResultAction;
    public void OnResult (Java.Lang.Object result) {
      if (OnResultAction != null)
        OnResultAction (result);
    }
  }
}

