using FlightFinder.Contracts;
using IPE.SmsIrClient;
using System.Net.Http.Json;
using System.Text.Json;

namespace FlightFinder
{
	public class Flight
	{
		private readonly HttpClient _client = new HttpClient();
		private readonly Random _random = new Random();

		public async Task CheckFlights()
		{
			try
			{
				var url = "https://api.flytoday.ir/api/V1/Flight/SearchAnytime";

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
				reqContent.Headers.Add("x-token", "61774b6a356c4561474542476f6c46683354566f5671664c424d784e4a5962776c577655306e6f3755654f37494e7a3067584e3651533670447a4a5154576b6c");
				reqContent.Headers.Add("x-app", "www.flytoday.ir");
				reqContent.Headers.Add("x-path", "https://www.flytoday.ir/flight/any-time?departure=thr,1&arrival=syz,1&departureDate=any&adt=1&chd=0&inf=0&cabin=1&isDomestic=true&isAnyWhere=true");

				var agent = UserAgents.Agents[_random.Next(9)];
				_client.DefaultRequestHeaders.Add("user-agent", agent);

				var apiRes = await _client.PostAsync(url, reqContent);
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
						var smsService = new SmsIr("23uQvwg4NBujB25bcTlyr7gnxmmWUSqlZCuhLxtSpiIoKDYm");
						var smsResult = await smsService.LikeToLikeSendAsync(30002108000958, [text], ["9199018123"]);
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
