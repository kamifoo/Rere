using FlightModel = Rere.Core.Models.Flight.Flight;

namespace Rere.Core.Services.Flight;

public interface IFlightService
{
    // - GET /api/ﬂights: Retrieve all ﬂights
    Task<IEnumerable<FlightModel>> GetAllFlightAsync();

    // - GET /api/ﬂights/{id}: Retrieve a speciﬁc ﬂight by ID
    Task<FlightModel> GetFlightByIdAsync(int id);

    // - POST /api/ﬂights: Create a new ﬂight
    Task CreateFlightAsync(FlightModel flight);

    // - PUT /api/ﬂights/{id}: Update a speciﬁc ﬂight
    Task UpdateFlightAsync(FlightModel flight);

    // - DELETE /api/ﬂights/{id}: Delete a speciﬁc ﬂight
    Task DeleteFlightAsync(int id);
}