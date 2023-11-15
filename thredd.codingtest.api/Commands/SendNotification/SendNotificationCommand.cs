using System;
using CSharpFunctionalExtensions;
using MediatR;

namespace Thredd.CodingTest.Api.Commands.SendNotification;

public sealed class SendNotificationCommand : IRequest<Result<Guid>>
{
	public string To { get; }
	public string From { get; }
	public string Message { get; }
	public string NotificationType { get; }
	public Guid NotificationId { get; set; }

	public SendNotificationCommand(Guid notificationId, string to, string from, string message, string notificationType)
	{
		NotificationId = notificationId;
		To = to;
		From = from;
		Message = message;
		NotificationType = notificationType;
	}
}