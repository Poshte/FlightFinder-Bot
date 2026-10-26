namespace FlightFinder
{
	public class Program
	{
		private static Timer _timer;
		private static Flight _flight = new();

		static async Task Main(string[] args)
		{
			_timer = new Timer(async _ => await CallApi(), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

			Console.WriteLine("Checking for flights...");
			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}

		private static async Task CallApi()
		{
			Console.WriteLine("Api was called.");
			await _flight.CheckFlights();
		}
	}
}