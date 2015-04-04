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


namespace TwitterMonkey {
  [Activity(Label = "TwitterMonkey", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity {
    protected override void OnCreate (Bundle bundle) {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // Get our button from the layout resource,
      // and attach an event to it
      Button button = FindViewById<Button>(Resource.Id.myButton);
      
      button.Click += async (sender, e) => {
        var textView    = FindViewById<TextView>(Resource.Id.textView1);
        var jsonString  = await fetchJsonAsync(new Uri ("http://goo.gl/pJwOUS"));
        var tweets      = TweetConverter.ConvertAll(jsonString);
        var imageView   = FindViewById<ImageView>(Resource.Id.imageView1);
        Picasso.With(ApplicationContext).Load(tweets[0].User.ProfileImageUrl).Into(imageView);
        foreach (Tweet tweet in tweets) {
          textView.Text += string.Format("{0} : {1}\n",tweet.User.Name,tweet.Message);
        }
      };
    }

    private async Task<string> fetchJsonAsync (Uri uri) {
      HttpWebRequest request = new HttpWebRequest(uri);
      var resp = await request.GetResponseAsync();
      StreamReader reader = new StreamReader (resp.GetResponseStream());
      return await reader.ReadToEndAsync();
    }
  }
}


