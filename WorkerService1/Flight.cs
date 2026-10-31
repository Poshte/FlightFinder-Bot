using IPE.SmsIrClient;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FlightFinder
{
	public class Flight : IFlight
	{
		private readonly HttpClient _client = new HttpClient();
		private readonly Random _random = new Random();
		private readonly IConfiguration _configuration;

		public Flight(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task CheckFlights()
		{
			try
			{
				var agent = UserAgents.Agents[_random.Next(9)];
				_client.DefaultRequestHeaders.Add("user-agent", agent);
				var apiRes = await _client.GetAsync(_configuration["FlyToday:Url"]);
				apiRes.EnsureSuccessStatusCode();
				var resContent = await apiRes.Content.ReadAsStringAsync();

				if (string.IsNullOrWhiteSpace(resContent))
					return;

				var aiResponseText = await ProcessFlightDataWithAI(resContent);
				if (string.IsNullOrWhiteSpace(aiResponseText))
					return;

				var smsService = new SmsIr(_configuration["SMS:ApiKey"]);
				var smsResult = await smsService.LikeToLikeSendAsync(long.Parse(_configuration["LineNumber"]!), [aiResponseText], [_configuration["MobileNumber"]!]);
				return;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private async Task<string> ProcessFlightDataWithAI(string flightsData)
		{
			try
			{
				using var request = new HttpRequestMessage(HttpMethod.Post, _configuration["OpenAi:Url"]);
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["OpenAi:ApiKey"]);

				var payload = new
				{
					model = "gpt-4o-mini",
					temperature = 0.3,
					messages = new[]
					{
						new { role = "user", content = $"""
	  You are a travel alert assistant. Check all these flights from Tehran to Shiraz. 
	  Find the ones that are cheaper than {_configuration["BasePriceToCheck"]}. That is a strict rule.
	  Afterwards, sort them based on the flight duration, travel agency and the services they provice.
	  Generate a highly concise text (max 80 chars per flight) alerting the user when one or more cheap flights have been.
	  Include the price, travel agency, date and time of the flight in the text.
	  In case no desired flights were available, return an empty string.
	  """ },
						new { role = "user", content = $"Flights: {flightsData}" }
					},
				};

				request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

				using var response = await _client.SendAsync(request);
				var body = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					Console.WriteLine($"HTTP {(int)response.StatusCode}: {body}");
					Console.WriteLine($"Retry-After: {response.Headers.RetryAfter}");
					return string.Empty;
				}

				if (!response.IsSuccessStatusCode)
					return string.Empty;

				var responseString = await response.Content.ReadAsStringAsync();
				using var jsonDoc = JsonDocument.Parse(responseString);
				return jsonDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
			}
			catch
			{
				throw;
			}
		}
	}
}