using CSharpFunctionalExtensions;
using thredd.codingtest.core;

namespace Thredd.CodingTest.Application.NotificationChannels;

public interface INotificationChannel
{
	Task<Result> SendNotificationAsync(string recipient, string message, string sender);
	NotificationType NotificationType { get; }
}