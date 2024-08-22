Feature: Manage Flight Information

# GET /api/ﬂights: Retrieve all ﬂights

    Scenario: Retrieve all flights
        Given the flight API has been initialized with flight data
        When I send a GET request to /api/flights
        Then I should receive a 200 OK response
        And the response should contain a list of all flights

# GET /api/ﬂights/{id}: Retrieve a speciﬁc ﬂight by ID

    Scenario: Retrieve a specific flight by ID
        Given a flight with ID 1 exists
        When I send a GET request to /api/flights/1
        Then I should receive a 200 OK response
        And the response should contain the flight details with ID 1

# POST /api/ﬂights: Create a new ﬂight

    Scenario: Create a new flight
        Given I have a valid flight payload
        When I send a POST request to /api/flights with the payload
        Then I should receive a 201 Created response
        And the flight should be stored in the system

# PUT /api/ﬂights/{id}: Update an exis7ng ﬂight

    Scenario: Update an existing flight
        Given a flight with ID 1 exists
        And I have a valid update payload
        When I send a PUT request to /api/flights/1 with the payload
        Then I should receive a 200 OK response
        And the flight details should be updated

# DELETE /api/ﬂights/{id}: Delete a ﬂight

    Scenario: Delete a flight
        # Id 6 should be the new created flight
        Given a flight with ID 6 exists
        When I send a DELETE request to /api/flights/6
        Then I should receive a 204 No Content response
        And the flight should be removed from the system

# GET /api/ﬂights/search: Search ﬂights by various criteria (e.g., airline, departure/arrival airport, date range)

    Scenario: Search flights by airline criteria
        Given flights exist in the system
        When I send a GET request to search with single parameter /api/flights/search?airline=NZ,CA
        Then I should receive a 200 OK response
        And the response should contain flights matching the search criteria

    Scenario: Search flights by flight number criteria
        Given flights exist in the system
        When I send a GET request to search with single parameter /api/flights/search?flightNumber=NZ401,CA783
        Then I should receive a 200 OK response
        And the response should contain flights matching the search criteria

    Scenario: Search flights by id criteria
        Given flights exist in the system
        When I send a GET request to search with single parameter /api/flights/search?id=1,3
        Then I should receive a 200 OK response
        And the response should contain flights matching the search criteria

    Scenario: Search flights by flight status criteria
        Given flights exist in the system
        When I send a GET request to search with single parameter /api/flights/search?status=inair,landed
        Then I should receive a 200 OK response
        And the response should contain flights matching the search criteria

    Scenario: Search flights by multiple criteria
        Given flights exist in the system
        When I send a GET request to search with id and flight number parameters /api/flights/search?id=1,3&flightNumber=NZ401,CA783
        Then I should receive a 200 OK response
        And the response should contain flights matching the search criteria

    Scenario: Search flights by wrong parameter
        Given flights exist in the system
        When I send a GET request to search with single wrong parameter /api/flights/search?company=NZ,CA
        Then I should receive a 400 BadRequest response

    Scenario: Search flights by multiple wrong parameters
        Given flights exist in the system
        When I send a GET request to search with multiple wrong parameters /api/flights/search?company=NZ,CA&weather=sunny
        Then I should receive a 400 BadRequest response