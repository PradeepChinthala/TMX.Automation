@Regression
Feature: Temp Bundle
	In order to avoid silly mistakes

Background: 
	Given I go to dashboard

Scenario: Select Bundle Verify Index and My View [TC-123]	
	When I click on Bundles
	And I click on index view
	And I click on my view
	And I click on logout
		Then login page should display
