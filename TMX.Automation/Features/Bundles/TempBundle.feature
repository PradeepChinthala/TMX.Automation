@Regression
Feature: Temp Bundle
	In order to avoid silly mistakes


Scenario: Select Bundle Verify Index and My View [TC-123]
	Given I go to dashboard
	When I click on Bundles
	And I click on index view
	And I click on my view
	And I click on logout
		Then login page should display
