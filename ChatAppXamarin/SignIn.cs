using Android.App;
using Android.Gms.Tasks;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Firebase.Auth;

namespace ChatAppXamarin
{
	[Activity(Label = "@string/ApplicationName", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
	public class SignIn : AppCompatActivity, IOnCompleteListener
	{
		FirebaseAuth auth;

		public void OnComplete(Task task)
		{
			if (task.IsSuccessful)
			{
				Toast.MakeText(this, "Sign In Successfully", ToastLength.Short).Show();
				Finish();
			}
			else
			{
				Toast.MakeText(this, "Sign In Failed", ToastLength.Short).Show();
				Finish();
			}

		}

		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.SignIn);

			auth = FirebaseAuth.Instance;

			var editEmail = FindViewById<EditText>(Resource.Id.txtEmail);
			var editPassword = FindViewById<EditText>(Resource.Id.txtPassword);
			var btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
			btnRegister.Click += delegate // onClickListener
			{
				auth.CreateUserWithEmailAndPassword(editEmail.Text, editPassword.Text).AddOnCompleteListener(this);

			};

		}
	}
}