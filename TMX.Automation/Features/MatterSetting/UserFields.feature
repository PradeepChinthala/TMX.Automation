@Regression
Feature: UserFields
	In order to Add,Edit,Delete and permissions for user fileds

Background: 
	Given I Go to dashboard

Scenario: [TC-264027]
Delete User Fields 
	Given I Select Matter
	When I Goto Matter Setting
	And I Click Fields
	Given I Click AddField 
	And I Enter Fields
	| nameField   | values   |
	| ShortName   | AA       |
	| FullName    | AA       |
	| DataType    | null     |
	| ContentType | null |
		Then Field Should Be Created
	When I Click Edit Icon Beside Field	
		Then str Should Be Displayed
	When I Click Delete Button
		Then Str Field Should Be Deleted


Scenario Outline: [TC-264028]
Delete User Fields 
	Given I Select Matter
	When I Goto Matter Setting
	And I Click Fields
	Given I Click AddField 
	And I Enter Fields
	| nameField   | values   |
	| ShortName   | AA       |
	| FullName    | AA       |
	| DataType    | null     |
	| ContentType | null |
		Then Field Should Be Created
Examples: 
| Test Case |
| 1234      |
| 3456      |
	

	
