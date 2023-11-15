using System;
using CSharpFunctionalExtensions;
using MediatR;

namespace thredd.codingtest.api.Queries.RetrieveNotificationStatus
{
	public class GetNotificationStatusQuery : IRequest<Result<string>>
	{
		public Guid NotificationId { get; }

		public GetNotificationStatusQuery(Guid notificationId)
		{
			NotificationId = notificationId;
		}
	}
}