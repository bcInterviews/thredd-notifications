using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Thredd.CodingTest.Api.Commands.SendNotification;
using thredd.codingtest.application.Emails;
using Thredd.CodingTest.Application.NotificationChannels;
using thredd.codingtest.infrastructure.Smtp;

namespace thredd.codingtest.api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			//services.AddDbContext<NotificationsDbContext>(options => {
			// options.UseSqlServer(Configuration.GetConnectionString("Database"));
			//});
			services.Configure<SmtpConfiguration>(Configuration.GetSection("SmtpConfiguration"));
			services.AddScoped<ISmtpEmailSender, MailKitSmtpEmailSender>();
			services.AddScoped<INotificationChannel, SmtpNotificationChannel>();
			services.AddValidatorsFromAssemblyContaining<SendNotificationCommandValidator>();
			services.AddScoped<INotificationChannelSelector, NotificationChannelSelector>();
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SendNotificationCommandHandler).Assembly));
		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
