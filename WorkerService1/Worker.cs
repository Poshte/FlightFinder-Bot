namespace FlightFinder
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly IFlight _flightBot;

		public Worker(ILogger<Worker> logger, IFlight flightBot)
		{
			_logger = logger;
			_flightBot = flightBot;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				if (_logger.IsEnabled(LogLevel.Information))
				{
					_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				}

				await _flightBot.CheckFlights();

				await Task.Delay(1000 * 60, stoppingToken);
			}
		}
	}
}
