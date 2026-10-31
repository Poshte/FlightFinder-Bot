using FlightFinder.Contracts;
using IPE.SmsIrClient;
using System.Net.Http.Json;
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
				var request = new SearchAnyTimeRequest()
				{
					pricingSourceType = 0,
					adultCount = 1,
					childCount = 0,
					infantCount = 0,

					travelPreference = new TravelPreference
					{
						cabinType = "Y",
						maxStopsQuantity = "All",
						airTripType = "OneWay"
					},

					originDestinationInformations = new List<OriginDestinationInformation>()
					{
						new OriginDestinationInformation()
						{
							destinationLocationCode= "SYZ",
							destinationType= "1",
							originLocationCode= "THR",
							originType= "1"
						}
					},

					isJalali = true
				};

				var reqContent = JsonContent.Create(request);
				reqContent.Headers.Add("x-token", _configuration["FlyToday:Token"]);
				reqContent.Headers.Add("x-app", "www.flytoday.ir");
				reqContent.Headers.Add("x-path", _configuration["FlyToday:Url"]);

				var agent = UserAgents.Agents[_random.Next(9)];
				_client.DefaultRequestHeaders.Add("user-agent", agent);

				var apiRes = await _client.PostAsync(_configuration["FlyToday:Url"], reqContent);
				apiRes.EnsureSuccessStatusCode();
				var resContent = await apiRes.Content.ReadAsStringAsync();

				var response = JsonSerializer.Deserialize<SearchAnyTimeResponse>(resContent);
				if (response == null)
					return;

				foreach (var pricedItinerary in response.pricedItineraries)
				{
					var flightPrice = pricedItinerary.airItineraryPricingInfo.itinTotalFare.totalFare;

					if (flightPrice <= 20000000)
					{
						var text = "Found a cheap flight dude! Check it out";
						var smsService = new SmsIr(_configuration["SMS:ApiKey"]);
						var smsResult = await smsService.LikeToLikeSendAsync(long.Parse(_configuration["LineNumber"]!), [text], [_configuration["MobileNumber"]]);
						return;
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
