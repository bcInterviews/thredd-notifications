using CSharpFunctionalExtensions;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Polly;
using Polly.Retry;
using thredd.codingtest.application.Emails;

namespace thredd.codingtest.infrastructure.Smtp;

public sealed class MailKitSmtpEmailSender : ISmtpEmailSender
{
    private readonly SmtpConfiguration _smtpConfiguration;
    private readonly AsyncRetryPolicy _retryPolicy;
    public MailKitSmtpEmailSender(IOptions<SmtpConfiguration> mailEngineConfiguration)
    {
        _smtpConfiguration = mailEngineConfiguration.Value;
        _retryPolicy = Policy
            .Handle<InvalidOperationException>()
            .Or<IOException>()
            .Or<ServiceNotConnectedException>()
            .Or<CommandException>()
            .WaitAndRetryAsync(_smtpConfiguration.RetryCount,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(_smtpConfiguration.WaitBetweenRetriesToThePowerOfInSeconds, retryAttempt)));
    }

    public async Task<Result> SendAsync(EmailContent emailContent)
    {
        try
        {
            if (emailContent == null) throw new ArgumentNullException(nameof(emailContent));
            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpConfiguration.Host, _smtpConfiguration.Port, SecureSocketOptions.StartTls);
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(emailContent.From, emailContent.From));
            mail.To.Add(new MailboxAddress(emailContent.To, emailContent.To));
            mail.Subject = emailContent.Subject;
            mail.Body = new TextPart(TextFormat.Plain) { Text = $"{emailContent.Message}" };
            await _retryPolicy.ExecuteAsync(async () => await client.SendAsync(mail));
            await client.DisconnectAsync(true);
            return Result.Success();
        }
        catch (Exception e) when (e is ObjectDisposedException or ArgumentException or ServiceNotConnectedException
                                      or CommandException or ProtocolException or IOException or InvalidOperationException
                                      or ServiceNotAuthenticatedException or ServiceNotConnectedException)
        {
            return Result.Failure(
                $"Unable to send email, error: {e.Message} {e.StackTrace}");
        }

        return Result.Success();
    }
}