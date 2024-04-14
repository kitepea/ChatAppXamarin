using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;

namespace ChatAppXamarin
{
	internal class ListViewAdapter : BaseAdapter
	{
		private List<MessageContent> lsMessage;
		private MainActivity mainActivity;
		public ListViewAdapter(MainActivity mainActivity, List<MessageContent> lsMessageContents)
		{
			this.mainActivity = mainActivity;
			this.lsMessage = lsMessageContents;
		}

		public override int Count
		{
			get { return lsMessage.Count; }
		}

		public override Object GetItem(int position)
		{
			return position;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			LayoutInflater inflater = (LayoutInflater)mainActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
			View itemView = inflater.Inflate(Resource.Layout.List_item, null);

			TextView message_user, message_time, message_content;
			message_user = itemView.FindViewById<TextView>(Resource.Id.message_user);
			message_content = itemView.FindViewById<TextView>(Resource.Id.message_text);
			message_time = itemView.FindViewById<TextView>(Resource.Id.message_time);

			message_user.Text = lsMessage[position].Email;
			message_time.Text = lsMessage[position].Time;
			message_content.Text = lsMessage[position].Message;

			return itemView;
		}
	}
}