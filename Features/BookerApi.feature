﻿Feature: BookerApi

Booker API tests

Scenario: Create Auth Token
	Given I extract the username and password from the JSON file
	When I call the auth API endpoint
	Then I receive a token within the response
