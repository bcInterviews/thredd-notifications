using CSharpFunctionalExtensions;
using thredd.codingtest.core;

namespace Thredd.CodingTest.Application.NotificationChannels;

public sealed class NotificationChannelSelector : INotificationChannelSelector
{
	private readonly IEnumerable<INotificationChannel> _channels;

	public NotificationChannelSelector(IEnumerable<INotificationChannel> channels)
	{
		_channels = channels ?? throw new ArgumentNullException(nameof(channels));
	}

	public Maybe<INotificationChannel?> GetNotificationChannel(NotificationType notificationType)
	{
		var channel = _channels.FirstOrDefault(t => t.NotificationType == notificationType);
		return Maybe.From(channel);
	}
}