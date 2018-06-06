@Regression
Feature: Login
	In order to verify control access to the Matter
	As a user or authenticated

Background: 
	Given I Go to dashboard


Scenario:TC_1234_SuccessfulSigninAndDisplayDashboard 
	Given I Select Matter
	When I Logout From TMX
		Then login page should display
