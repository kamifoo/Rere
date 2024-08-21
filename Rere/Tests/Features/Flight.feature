Feature: Manage Flight Information

  Scenario: Retrieve all flights
    Given the flight API has been initialized with flight data
    When I send a GET request to /api/flights
    Then I should receive a 200 OK response
    And the response should contain a list of all flights

#  Scenario: Retrieve a specific flight by ID
#    Given a flight with ID 1 exists
#    When I send a GET request to /api/flights/1
#    Then I should receive a 200 OK response
#    And the response should contain the flight details with ID 1
#
#  Scenario: Create a new flight
#    Given I have a valid flight payload
#    When I send a POST request to /api/flights with the payload
#    Then I should receive a 201 Created response
#    And the flight should be stored in the system
#
#  Scenario: Update an existing flight
#    Given a flight with ID 1 exists
#    And I have a valid update payload
#    When I send a PUT request to /api/flights/1 with the payload
#    Then I should receive a 200 OK response
#    And the flight details should be updated
#
#  Scenario: Delete a flight
#    Given a flight with ID 1 exists
#    When I send a DELETE request to /api/flights/1
#    Then I should receive a 204 No Content response
#    And the flight should be removed from the system
#
#  Scenario: Search flights by criteria
#    Given flights exist in the system
#    When I send a GET request to /api/flights/search with search parameters
#    Then I should receive a 200 OK response
#    And the response should contain flights matching the search criteria