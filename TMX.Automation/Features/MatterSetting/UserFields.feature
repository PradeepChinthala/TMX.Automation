@Regressions
Feature: UserFields
	In order to Add,Edit,Delete and permissions for user fileds

Background: 
	Given I Go to dashboard

Scenario: TC_264027_DeleteUserFields 
	Given I Select Matter
	When I Goto Matter Setting
	And I Click Fields Tab
	And I Click AddField 
	And I Enter Fields
	| nameField   | values |
	| ShortName   | AA     |
	| FullName    | AA     |
	| DataType    | null   |
	| ContentType | null   |
		Then Field Should Be Created
	When I Click Edit Icon Beside Field
		Then str Should Be Displayed
	When I Click Delete Button
		Then Str Field Should Be Deleted
		And I Logout From TMX


Scenario Outline: TC-264028_DeleteUserFields 
	Given I Select Matter
	When I Goto Matter Setting
	And I Click Fields Tab
	Then I Logout From TMX
Examples: 
| Test Case |
| 1234      |
	

	
