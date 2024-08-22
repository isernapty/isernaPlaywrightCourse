Feature: TestContainersMySQLFeature

Store and read credentials from mySQL container

Scenario: Store then read credentials
	Given I run the mySql test container
	Given I store credentials
	When I read credentials
