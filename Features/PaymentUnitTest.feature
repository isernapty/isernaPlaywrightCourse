Feature: PaymentUnitTest

Test the GetPayRate function

Scenario: Monday Pay Rate Test
	Given its monday
	When I calculate the rate of the pay for 10.00m
	Then I obtain the usual pay: 10.00m

Scenario: Sunday Pay Rate Test
	Given its sunday
	When I calculate the rate of the pay for 10.00m
	Then I obtain a 25% higher pay: 12.5m