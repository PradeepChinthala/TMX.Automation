@Regression
Feature: Dashboard
	In order to verify control access to the Matter
	As a user or authenticated

Background: 
	Given I Go to dashboard


Scenario:TC-1234_SuccessfulSigninAndDisplayDashboard 
	Given I Select Matter
	When I Logout From TMX
		Then login page should display
