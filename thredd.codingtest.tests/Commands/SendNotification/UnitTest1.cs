using System;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Thredd.CodingTest.Api.Commands.SendNotification;
using Xunit;

namespace thredd.codingtest.tests.Commands.SendNotification
{
    public class UnitTest1
    {
		[Theory]
		[InlineData("fdsfsdfds")]
		public async Task To_ShouldBeValidatedAsEmailAddress_WhenNotificationType_IsEmail(string to)
		{
			var validator = new SendNotificationCommandValidator();
			var command = new SendNotificationCommand(Guid.NewGuid(), to, "test", "test", "Email");
			var result = await validator.TestValidateAsync(command);
			result.ShouldHaveValidationErrorFor(x => x.To);
		}
	}
}
