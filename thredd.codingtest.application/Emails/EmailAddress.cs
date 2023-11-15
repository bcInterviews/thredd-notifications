namespace thredd.codingtest.application.Emails
{
	public sealed record EmailAddress
	{
		public string Value { get; }

		public EmailAddress(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
			Value = value;
		}
	}
}