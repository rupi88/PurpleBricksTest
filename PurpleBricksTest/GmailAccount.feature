Feature: GmailAccount
	As a customer
	I want to register a Gmail account
	So that I can send and receive emails

Scenario: Create a valid Gmail account
	Given I am on the Gmail homepage
	And I have selected to create an account
	When I enter my personal details
	Then I will have sucessfully created my new account

Scenario: Create account with invalid email address
	Given I am on the Gmail homepage
	And I have selected to create an account
	When I enter an invalid email address
	Then I will be presented with an email validation error

Scenario: Login and Sign out of my account
	Given I am on the Gmail homepage
	And I have logged into my account
	When I select Log Out
	Then I will have sucessfully logged out of my account
