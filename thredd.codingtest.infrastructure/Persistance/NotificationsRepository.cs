using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Polly.Retry;
using Polly;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace thredd.codingtest.infrastructure.Persistance
{
	public sealed class NotificationsRepository
	{
		private readonly DatabaseSettings _configuration;
		private readonly AsyncRetryPolicy _retryPolicy;

		public NotificationsRepository(IOptions<DatabaseSettings> configuration)
		{
			_configuration = configuration?.Value ??
			                 throw new ArgumentException("Options or its configuration value is null",
				                 nameof(configuration));

			_retryPolicy = Policy
				.Handle<SqlException>()
				.Or<TimeoutException>()
				.Or<InvalidOperationException>()
				.WaitAndRetryAsync(_configuration.RetryCount,
					sleepDurationProvider: retryAttempt =>
						TimeSpan.FromSeconds(Math.Pow(_configuration.WaitBetweenRetriesToThePowerOfInSeconds,
							retryAttempt)));
		}

		public async Task<Result> Add(Guid notificationId)
		{
			try
			{
				await _retryPolicy.ExecuteAsync(async () =>
				{
					var parameters = new DynamicParameters();
					parameters.Add("@NotificationId", notificationId);
					await using var databaseConnection = new SqlConnection(_configuration.ConnectionString);
					await databaseConnection.ExecuteAsync("Notifications.InsertNotification", parameters,
						commandType: CommandType.StoredProcedure,
						commandTimeout: _configuration.CommandTimeoutInSeconds);
				});

				return Result.Success();
			}
			catch (Exception e) when (e is SqlException or NullReferenceException or TimeoutException or
				                          InvalidOperationException or TimeoutException or InvalidOperationException)
			{
				return Result.Failure(
					$"Unable to persist notification {notificationId}, error {e.Message} {e.StackTrace}");
			}
		}
	}
}
