Feature: ApiTest

API test example

Scenario: Test GET API Call
	Given I prepare the query vars for the request
	When I send the GET request to the API
	Then I receive the response with a status 200
