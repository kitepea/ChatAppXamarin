using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;

namespace ChatAppXamarin
{
	[Activity(Label = "@string/ApplicationName", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity, IValueEventListener
	{
		private FirebaseClient firebase;
		private List<MessageContent> lsMessageContents = new List<MessageContent>();
		private EditText editChat;
		private ListView lsChat;
		private ImageButton fab;

		public int MyResultCode = 1;

		public void OnCancelled(DatabaseError error)
		{
			throw new System.NotImplementedException();
		}

		public void OnDataChange(DataSnapshot snapshot)
		{
			DisplayChatMessage();
		}

		private async void DisplayChatMessage()
		{
			lsMessageContents.Clear();
			var items = await firebase.Child("chats")
				.OnceAsync<MessageContent>();

			foreach (var item in items)
				lsMessageContents.Add(item.Object);
			ListViewAdapter adapter = new ListViewAdapter(this, lsMessageContents);
			lsChat.Adapter = adapter;
		}

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			if (FirebaseApp.Instance == null)
			{
				var options = new Firebase.FirebaseOptions.Builder()
					.SetApplicationId("xamarin-chat-app-54bb9")
					.SetApiKey("AIzaSyA5fm-no6Htk1DhMwv-CzusadBUOn0LFsg")
					.SetDatabaseUrl("https://xamarin-chat-app-54bb9-default-rtdb.firebaseio.com/")
					.SetStorageBucket("gs://xamarin-chat-app-54bb9.appspot.com")
					.Build();

				var app = FirebaseApp.InitializeApp(Application.Context, options);
			}


			firebase = new FirebaseClient("https://xamarin-chat-app-54bb9-default-rtdb.firebaseio.com/");
			FirebaseDatabase.Instance.GetReference("chats").AddValueEventListener(this);

			fab = FindViewById<ImageButton>(Resource.Id.fab);
			editChat = FindViewById<EditText>(Resource.Id.input);
			lsChat = FindViewById<ListView>(Resource.Id.list_of_messages);

			fab.Click += delegate
			{
				PostMessage();
			};


			if (FirebaseAuth.Instance.CurrentUser == null)
				StartActivityForResult(new Android.Content.Intent(this, typeof(SignIn)), MyResultCode);
			else
			{
				Toast.MakeText(this, "Welcome " + FirebaseAuth.Instance.CurrentUser.Email, ToastLength.Short).Show();
				DisplayChatMessage();
			}
		}

		private async void PostMessage()
		{
			var message = new MessageContent(FirebaseAuth.Instance.CurrentUser.Email, editChat.Text);
			var jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
			var items = await firebase.Child("chats").PostAsync(jsonMessage);
			editChat.Text = "";
		}
	}
}