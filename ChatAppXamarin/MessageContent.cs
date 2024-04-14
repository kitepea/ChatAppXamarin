using System;

namespace ChatAppXamarin
{
	internal class MessageContent
	{

		public string Message { get; set; }
		public string Email { get; set; }
		public string Time { get; set; }
		public MessageContent() { }

		public MessageContent(string email, string message)
		{
			this.Message = message;
			this.Email = email;
			this.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}