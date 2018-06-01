@Regression
Feature: Dashboard
	In order to verify control access to the Matter
	As a user or authenticated

Background: 
	Given I Go to dashboard


Scenario:[TC-1234]
Successful sign in and display dashboard 
	When I click on logout
		Then login page should display
