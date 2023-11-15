using CSharpFunctionalExtensions;
using thredd.codingtest.core;

namespace Thredd.CodingTest.Application.NotificationChannels;

public interface INotificationChannelSelector
{
	Maybe<INotificationChannel?> GetNotificationChannel(NotificationType notificationType);
}