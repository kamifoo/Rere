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

    [Given(@"the flight API has been initialized with flight data")]
    public void GivenTheFlightApiHasBeenInitializedWithFlightData()
    {
        // TODO Think any init?
    }

    [When(@"I send a GET request to /api/flights")]
    public async Task WhenISendAGetRequestToApiFlights()
    {
        _response = await _client.GetAsync("api/flights");
        if (_response == null) throw new Exception("Error: HTTP client unexpectedly returns a null response.");
    }

    [Then(@"I should receive a (.*) OK response")]
    public void ThenIShouldReceiveAOkResponse(int statusCode)
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
    }

    [When(@"I send a GET request to /api/flights/(.*)")]
    public async Task WhenISendAGetRequestToApiFlightWithId(int id)
    {
        _response = await _client.GetAsync($"api/flights/{id}");
        if (_response == null) throw new Exception("Error: HTTP client unexpectedly returns a null response.");
    }

    [Then(@"the response should contain the flight details with ID (.*)")]
    public async Task ThenTheResponseShouldContainTheFlightWithCorrectId(int id)
    {
        var flight = await _response!.Content.ReadAsStringAsync();
        flight.Should().NotBeNullOrEmpty();
        var flightDto = flight.FromJson<GetFlightDto>();
        flightDto.Id.Should().Be(id);
    }
}