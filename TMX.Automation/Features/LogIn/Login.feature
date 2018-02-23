@LoginFeature
Feature: Login
	In order to verify control access to the Matter
	As a user or authenticated

Background: 
	Given I go to dashboard

@SP1
Scenario:Successful sign in and display dashboard [TC-1234]
	When I click on logout
		Then login page should display
