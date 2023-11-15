using System;

namespace thredd.codingtest.api.Dto
{
	public class NotificationEventDto
	{
		public Guid Id { get; set; } = new Guid();

		public string To { get; set; }

		public string From { get; set; }

		public string Message { get; set; }

		public string NotificationType { get; set; }

		public DateTime Created { get; set; } = DateTime.Now;
	}
}