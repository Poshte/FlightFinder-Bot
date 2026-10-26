namespace FlightFinder.Contracts
{
	public class SearchAnyTimeResponse
	{
		public int searchId { get; set; }
		public List<PricedItinerary> pricedItineraries { get; set; }
		public AdditionalData additionalData { get; set; }
		public string airTripType { get; set; }
		public string airTripTypeStr { get; set; }
		public int traceId { get; set; }
	}

	public class AdditionalData
	{
		public List<Airline> airlines { get; set; }
		public List<Airport> airports { get; set; }
		public List<DateTime> departureDates { get; set; }
		public object returnDates { get; set; }
	}

	public class AirItineraryPricingInfo
	{
		public string fareType { get; set; }
		public ItinTotalFare itinTotalFare { get; set; }
		public List<PtcFareBreakdown> ptcFareBreakdown { get; set; }
		public List<object> fareInfoes { get; set; }
	}

	public class Airline
	{
		public string iata { get; set; }
		public string name { get; set; }
		public string nameFa { get; set; }
	}

	public class Airport
	{
		public string iata { get; set; }
		public string name { get; set; }
		public string nameFa { get; set; }
		public string cityFa { get; set; }
		public string cityId { get; set; }
		public string countryCode { get; set; }
		public string countryFa { get; set; }
		public string latitude { get; set; }
		public string longitude { get; set; }
	}

	public class FlightSegment
	{
		public DateTime departureDateTime { get; set; }
		public DateTime arrivalDateTime { get; set; }
		public string departureTerminal { get; set; }
		public string arrivalTerminal { get; set; }
		public int stopQuantity { get; set; }
		public string flightNumber { get; set; }
		public string resBookDesigCode { get; set; }
		public string journeyDuration { get; set; }
		public int journeyDurationPerMinute { get; set; }
		public int connectionTimePerMinute { get; set; }
		public string departureAirportLocationCode { get; set; }
		public string departureAirportCityId { get; set; }
		public string arrivalAirportLocationCode { get; set; }
		public string arrivalAirportCityId { get; set; }
		public string marketingAirlineCode { get; set; }
		public string cabinClassCode { get; set; }
		public string cabinClassName { get; set; }
		public string cabinClassNameFa { get; set; }
		public OperatingAirline operatingAirline { get; set; }
		public int? seatsRemaining { get; set; }
		public bool isCharter { get; set; }
		public bool isReturn { get; set; }
		public string baggage { get; set; }
		public string baggageFa { get; set; }
		public List<object> technicalStops { get; set; }
		public List<object> flightAmenities { get; set; }
	}

	public class ItinTotalFare
	{
		public int totalService { get; set; }
		public int totalFare { get; set; }
		public int grandTotalWithoutDiscount { get; set; }
		public int totalBaseFare { get; set; }
		public object discountAmount { get; set; }
		public int totalVat { get; set; }
		public int totalTax { get; set; }
		public int totalServiceTax { get; set; }
		public int totalCommission { get; set; }
		public int totalSurcharge { get; set; }
	}

	public class OperatingAirline
	{
		public string code { get; set; }
		public string flightNumber { get; set; }
		public string equipment { get; set; }
		public string equipmentName { get; set; }
	}

	public class OriginDestinationOption
	{
		public int journeyDurationPerMinute { get; set; }
		public int connectionTimePerMinute { get; set; }
		public List<FlightSegment> flightSegments { get; set; }
	}

	public class PassengerFare
	{
		public int baseFare { get; set; }
		public int totalFare { get; set; }
		public int serviceTax { get; set; }
		public List<object> taxes { get; set; }
		public int total { get; set; }
		public int tax { get; set; }
		public int vat { get; set; }
		public int baseFareWithMarkup { get; set; }
		public int totalFareWithMarkupAndVat { get; set; }
		public int commission { get; set; }
		public int priceCitizen { get; set; }
		public List<object> surcharge { get; set; }
	}

	public class PassengerTypeQuantity
	{
		public string passengerType { get; set; }
		public int quantity { get; set; }
	}

	public class PayLater
	{
		public bool hasPayLater { get; set; }
	}

	public class PricedItinerary
	{
		public string passportMandatoryType { get; set; }
		public string domesticCountryCode { get; set; }
		public bool isPassportMandatory { get; set; }
		public bool isDestinationAddressMandatory { get; set; }
		public bool isPassportIssueDateMandatory { get; set; }
		public int directionInd { get; set; }
		public string refundMethod { get; set; }
		public string validatingAirlineCode { get; set; }
		public string fareSourceCode { get; set; }
		public bool hasFareFamilies { get; set; }
		public AirItineraryPricingInfo airItineraryPricingInfo { get; set; }
		public List<OriginDestinationOption> originDestinationOptions { get; set; }
		public object featured { get; set; }
		public int bestExperienceIndex { get; set; }
		public bool isCharter { get; set; }
		public bool isSystem { get; set; }
		public bool isInstance { get; set; }
		public bool isOffer { get; set; }
		public bool isSeatServiceMandatory { get; set; }
		public bool isMealServiceMandatory { get; set; }
		public bool hasAmenities { get; set; }
		public string sellingStrategy { get; set; }
		public List<object> visaRequirements { get; set; }
		public bool isClosed { get; set; }
		public bool hasPermissionForReservation { get; set; }
		public int clubPoint { get; set; }
		public PayLater payLater { get; set; }
		public TravelRestriction travelRestriction { get; set; }
		public object routeInfo { get; set; }
		public bool hasCancellationGuarantee { get; set; }
	}

	public class PtcFareBreakdown
	{
		public PassengerFare passengerFare { get; set; }
		public PassengerTypeQuantity passengerTypeQuantity { get; set; }
	}

	public class TravelRestriction
	{
		public bool success { get; set; }
		public int airTripType { get; set; }
		public object originLocationCode { get; set; }
		public object destinationLocationCode { get; set; }
		public object stopConnectionLocationCode { get; set; }
		public List<object> travelRestrictionItems { get; set; }
	}
}