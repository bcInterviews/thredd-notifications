using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thredd.CodingTest.Api.Commands.SendNotification;
using thredd.codingtest.api.Dto;
using thredd.codingtest.api.Queries.RetrieveNotificationStatus;

namespace thredd.codingtest.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly IMediator _mediator;

		public NotificationController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpPost]
		public async Task<IActionResult> SendNotification([FromBody] NotificationEventDto notificationEvent)
		{
			var sendNotificationCommand = new SendNotificationCommand(notificationEvent.Id, notificationEvent.To, notificationEvent.From,
				notificationEvent.Message, notificationEvent.NotificationType);
			var sendResult = await _mediator.Send(sendNotificationCommand);

			if (sendResult.IsFailure)
			{
				return BadRequest(sendResult.Error);
			}

			return Ok("Message Sent");
		}

		[HttpGet]
		[Route("status/{id}")]
		public async Task<IActionResult> GetStatus([FromRoute] Guid id)
		{
			var notificationStatusResult = await _mediator.Send(new GetNotificationStatusQuery(id));

			return notificationStatusResult.IsFailure
				? StatusCode(StatusCodes.Status500InternalServerError)
				: Ok(notificationStatusResult.Value);
		}
	}
}