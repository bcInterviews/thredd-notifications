using System;
using FluentValidation;
using thredd.codingtest.core;

namespace Thredd.CodingTest.Api.Commands.SendNotification;

public class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
	public SendNotificationCommandValidator()
	{
		RuleFor(x => x.From).NotEmpty();
		RuleFor(x => x.To).NotEmpty();
		RuleFor(x => x.Message).NotEmpty();
		RuleFor(x => x.NotificationType)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.Must(nt => Enum.TryParse(nt, out NotificationType notificationType)).WithMessage("Invalid Notification type");

		When(x => Enum.TryParse(x.NotificationType, out NotificationType notificationType) && notificationType == NotificationType.Email, () =>
		{
			RuleFor(x => x.From).EmailAddress();
			RuleFor(x => x.To).EmailAddress();
		});
	}
}