using CSharpFunctionalExtensions;
using thredd.codingtest.application.Emails;
using thredd.codingtest.core;

namespace Thredd.CodingTest.Application.NotificationChannels;

public class SmtpNotificationChannel : INotificationChannel
{
	private readonly ISmtpEmailSender _smtpEmailSender;

	public SmtpNotificationChannel(ISmtpEmailSender smtpEmailSender)
	{
		_smtpEmailSender = smtpEmailSender ?? throw new ArgumentNullException(nameof(smtpEmailSender));
	}

	public async Task<Result> SendNotificationAsync(string recipient, string message, string sender)
	{
		try
		{
			var emailContent = new EmailContent(new EmailAddress(recipient), new EmailAddress(sender), message,
				"Email notification");

			var sendEmailResult = await _smtpEmailSender.SendAsync(emailContent);
			return sendEmailResult;
		}
		catch (ArgumentException e)
		{
			return Result.Failure($"Failed to create or send email, error: {e.Message} {e.StackTrace}");
		}
	}

	public NotificationType NotificationType => NotificationType.Email;
}