namespace thredd.codingtest.infrastructure.Smtp
{
    public sealed class SmtpConfiguration
    {
        public bool UseSsl;
        public string Host { get; set; }
        public int Port { get; set; }
        public int WaitBetweenRetriesToThePowerOfInSeconds { get; set; }
        public int RetryCount { get; set; }
    }
}