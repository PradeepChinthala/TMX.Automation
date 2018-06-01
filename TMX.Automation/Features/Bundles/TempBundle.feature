@Regression
Feature: Temp Bundle
	In order to avoid silly mistakes

Background: 
	Given I Go to dashboard

Scenario: [TC-123]
Select Bundle Verify Index and My View 
	When I Select Matter for US#246152-5
	When I click on Bundles
	And I click on index view
	And I click on my view
	And I click on logout
		Then login page should display
