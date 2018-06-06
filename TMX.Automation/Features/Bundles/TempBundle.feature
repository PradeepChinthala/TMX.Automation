@Regression
Feature: Temp Bundle
	In order to avoid silly mistakes

Background: 
	Given I Go to dashboard

Scenario: TC_23_SelectBundleVerifyIndexAndMyView 
	Given I Select Matter
	When I click on Bundles
	And I click on index view
	And I click on my view
	And I Logout From TMX
		Then login page should display
