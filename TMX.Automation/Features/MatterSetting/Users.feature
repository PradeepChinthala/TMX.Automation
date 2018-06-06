
Feature: Users
In order to Add,Edit,Delete Users

Scenario:TC_264037_VerifyPemissionsForGuestUser
	Given I Go to dashboard
	And I Select Matter
	When I Goto Matter Setting
	And I Click Users Tab
		Then I Logout From TMX
