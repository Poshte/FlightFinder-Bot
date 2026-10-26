namespace FlightFinder.Contracts
{
	public class SearchAnyTimeRequest
	{
		public int pricingSourceType { get; set; }
		public int adultCount { get; set; }
		public int childCount { get; set; }
		public int infantCount { get; set; }
		public TravelPreference travelPreference { get; set; }
		public List<OriginDestinationInformation> originDestinationInformations { get; set; }
		public bool isJalali { get; set; }
	}

	public class OriginDestinationInformation
	{
		public string destinationLocationCode { get; set; }
		public string destinationType { get; set; }
		public string originLocationCode { get; set; }
		public string originType { get; set; }
	}

	public class TravelPreference
	{
		public string cabinType { get; set; }
		public string maxStopsQuantity { get; set; }
		public string airTripType { get; set; }
	}
}
