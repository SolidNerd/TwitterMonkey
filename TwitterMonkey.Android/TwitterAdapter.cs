using System.Collections.Generic;
using Android.App;
using Android.Widget;
using TwitterMonkey.Portable.Twitter;
using Android.Views;
using Squareup.Picasso;

namespace TwitterMonkey {
  public class TwitterAdapter : BaseAdapter<Tweet> {

    private List<Tweet> tweets;
    private Activity context;

    public TwitterAdapter(Activity context, List<Tweet> tweets) : base() {
      this.context = context;
      this.tweets = tweets;
    }

    public override long GetItemId(int position) {
      return position;
    }

    public override Tweet this[int position] {
      get { return tweets[position]; }
    }

    public override int Count {
      get { return tweets.Count; }
    }

    public override View GetView(int position, View convertView, ViewGroup parent) {
      var item = tweets[position];
      View view = convertView;
      // no view to re-use, create new
      if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.TweetView, null);
      view.FindViewById<TextView>(Resource.Id.TweetUserName).Text = item.User.Name;
      view.FindViewById<TextView>(Resource.Id.TweetMessage).Text = item.Message;
      Picasso.With(context).Load(item.User.ProfileImageUrl).Into(view.FindViewById<ImageView>(Resource.Id.TweetUserImage));
      return view;
    }

    public void Update(List<Tweet> tweets){
      this.tweets.Clear();
      this.tweets.AddRange(tweets);
      NotifyDataSetChanged();
    }
  }
}

