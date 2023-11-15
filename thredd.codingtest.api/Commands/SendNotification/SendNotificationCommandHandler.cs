using System;
using System.Threading.Tasks;
using System.Threading;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Thredd.CodingTest.Application.NotificationChannels;
using thredd.codingtest.core;

namespace Thredd.CodingTest.Api.Commands.SendNotification
{
	public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, Result<Guid>>
	{
		private readonly IValidator<SendNotificationCommand> _validator;
		private readonly INotificationChannelSelector _notificationChannelSelector;

		public SendNotificationCommandHandler(IValidator<SendNotificationCommand> validator, INotificationChannelSelector notificationChannelSelector)
		{
			_validator = validator ?? throw new ArgumentNullException(nameof(validator));
			_notificationChannelSelector = notificationChannelSelector ?? throw new ArgumentNullException(nameof(notificationChannelSelector));
		}
		public async Task<Result<Guid>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				return Result.Failure<Guid>(validationResult.ToString("~"));
			}

			var notificationType = Enum.Parse<NotificationType>(request.NotificationType);

			var notificationChannel = _notificationChannelSelector.GetNotificationChannel(notificationType);

			if (notificationChannel.HasNoValue) return Result.Failure<Guid>("Invalid notification type");

			var sendResult = await notificationChannel.Value.SendNotificationAsync(request.From, request.Message, request.To);

			return sendResult.IsFailure ? Result.Failure<Guid>($"Notification{request.NotificationId} {sendResult.Error}") : Result.Success(request.NotificationId);
		}
	}
}