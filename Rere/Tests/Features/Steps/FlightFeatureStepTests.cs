using System.Net;
using FluentAssertions;
using Rere.DTOs.Flight;
using Rere.Tests.Fixtures;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;

namespace Rere.Tests.Features.Steps;

[Binding]
public class FlightFeatureStepTests(TestRereServerFixture fixture)
{
    private readonly HttpClient _client = fixture.Client;
    private HttpResponseMessage? _response;
    private object? _createFlightDto;
    private object? _updateFlightDto;
    private int _testingFlightId;
    private int _createdFlightId;

    [Given(@"the flight API has been initialized with flight data")]
    public void GivenTheFlightApiHasBeenInitializedWithFlightData()
    {
        // Skip this step as using Development config with seeding data
    }

    [When(@"I send a GET request to /api/flights")]
    public async Task WhenISendAGetRequestToApiFlights()
    {
        _response = await _client.GetAsync("api/flights");
    }

    [Then(@"I should receive a (.*) OK response")]
    public void ThenIShouldReceiveAOkResponse(int statusCode)
    {
        _response!.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"I should receive a (.*) Created response")]
    public void ThenIShouldReceiveACreatedResponse(int statusCode)
    {
        _response!.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"the response should contain a list of all flights")]
    public async Task ThenTheResponseShouldContainAListOfAllFlights()
    {
        var flights = await _response!.Content.ReadAsStringAsync();
        flights.Should().NotBeNullOrEmpty();
    }

    [Given(@"a flight with ID (.*) exists")]
    public void GivenAFlightWithIdExists(int id)
    {
        _testingFlightId = id;
    }

    [When(@"I send a GET request to /api/flights/(.*)")]
    public async Task WhenISendAGetRequestToApiFlightWithId(int id)
    {
        _response = await _client.GetAsync($"api/flights/{id}");
    }

    [Then(@"the response should contain the flight details with ID (.*)")]
    public async Task ThenTheResponseShouldContainTheFlightWithCorrectId(int id)
    {
        var flight = await _response!.Content.ReadAsStringAsync();
        flight.Should().NotBeNullOrEmpty();
        var flightDto = flight.FromJson<GetFlightDto>();
        flightDto.Id.Should().Be(id);
    }

    [Given(@"I have a valid flight payload")]
    public void GivenIHaveAValidFlightPayload()
    {
        _createFlightDto = new
        {
            FlightNumber = "NZ402",
            Airline = "NZ",
            DepartureAirport = "WLG",
            ArrivalAirport = "AKL",
            DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
            ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(1),
            Status = "Canceled"
        };
    }

    [When(@"I send a POST request to /api/flights with the payload")]
    public async Task WhenISendAPostRequestToApiFlightsWithThePayload()
    {
        var jsonContent = JsonContent.Create(_createFlightDto);
        _response = await _client.PostAsync("api/flights", jsonContent);
    }

    [Then(@"the flight should be stored in the system")]
    public async Task ThenTheFlightShouldBeStoredInTheSystem()
    {
        var result = await _response!.Content.ReadAsStringAsync();
        result.Should().NotBeNullOrEmpty();
        _createdFlightId = result.FromJson<int>()!;
    }

    [Given(@"I have a valid update payload")]
    public void GivenIHaveAValidUpdatePayload()
    {
        _updateFlightDto = new
        {
            Id = 1,
            FlightNumber = "NZ402",
            Airline = "NZ",
            DepartureAirport = "WLG",
            ArrivalAirport = "AKL",
            DepartureTime = TimeProvider.System.GetUtcNow().DateTime,
            ArrivalTime = TimeProvider.System.GetUtcNow().DateTime.AddHours(1),
            Status = "Canceled"
        };
    }

    [When(@"I send a PUT request to /api/flights/(.*) with the payload")]
    public async Task WhenISendAPutRequestToApiFlightsWithThePayload(int id)
    {
        var jsonContent = JsonContent.Create(_updateFlightDto);
        _response = await _client.PutAsync($"api/flights/{id}", jsonContent);
    }

    [Then(@"the flight details should be updated")]
    public async Task ThenTheFlightDetailsShouldBeUpdated()
    {
        var updatedFlightResult = await _client.GetAsync($"api/flights/{_testingFlightId}");
        var updatedFlight = await updatedFlightResult.Content.ReadAsStringAsync();
        var flightDto = updatedFlight.FromJson<GetFlightDto>();
        _updateFlightDto.Should().BeEquivalentTo(flightDto);
    }

    [When(@"I send a DELETE request to /api/flights/(.*)")]
    public async Task WhenISendADeleteRequestToApiFlights(int id)
    {
        _testingFlightId = id;
        _response = await _client.DeleteAsync($"api/flights/{id}");
    }

    [Then(@"I should receive a (.*) No Content response")]
    public void ThenIShouldReceiveANoContentResponse(int statusCode)
    {
        _response!.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"the flight should be removed from the system")]
    public async Task ThenTheFlightShouldBeRemovedFromTheSystem()
    {
        var getDeletedFlightResult = await _client.GetAsync($"api/flights/{_testingFlightId}");
        var deletedFlight = await getDeletedFlightResult.Content.ReadAsStringAsync();
        deletedFlight.Should().Contain("404");
    }
}