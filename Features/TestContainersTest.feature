Feature: TestContainersTest

Hello World Test Container

Scenario: Run and test Hello World Test Container
	Given I run the hello world test container
	When I make a request to the URL
	Then I obtain a valid UUID inside the response
