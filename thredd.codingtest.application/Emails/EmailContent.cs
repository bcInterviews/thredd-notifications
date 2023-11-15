namespace thredd.codingtest.application.Emails;

public sealed class EmailContent
{
	public string To { get; set; }
	public string From { get; set; }
	public string Subject { get; set; }
	public string Message { get; set; }

	public EmailContent(EmailAddress toAddress, EmailAddress fromAddress, string message, string subject)
	{
		if (toAddress == null) throw new ArgumentNullException(nameof(toAddress));
		if (fromAddress == null) throw new ArgumentNullException(nameof(fromAddress));
		if (string.IsNullOrWhiteSpace(message))
			throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));
		if (string.IsNullOrWhiteSpace(subject))
			throw new ArgumentException("Value cannot be null or whitespace.", nameof(subject));
	}
}