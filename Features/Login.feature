Feature: Login

Load login page and click on login button to be redirected to the main page logged in

Scenario: Standard User login
	Given I go to the main login page
	When I introduce the standard_user user login credentials
	And I click on login button
	Then I should see main page

Scenario: Locked User login
	Given I go to the main login page
	When I introduce the locked_out_user user login credentials
	And I click on login button
	Then I should see an error about the user being locked