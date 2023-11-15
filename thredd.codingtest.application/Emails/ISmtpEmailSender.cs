using CSharpFunctionalExtensions;

namespace thredd.codingtest.application.Emails;

public interface ISmtpEmailSender
{
	Task<Result> SendAsync(EmailContent emailContent);
}