using System.ComponentModel.DataAnnotations;

namespace thredd.codingtest.infrastructure.Persistance;

public sealed class DatabaseSettings
{
	[Required]
	[Range(1, 10)]
	public int RetryCount { get; set; }

	[Required]
	[Range(1, 5)]
	public int WaitBetweenRetriesToThePowerOfInSeconds { get; set; }

	[Required]
	public string ConnectionString { get; set; }

	[Required]
	public int CommandTimeoutInSeconds { get; set; }
}