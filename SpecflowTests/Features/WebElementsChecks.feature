Feature: WebElementsChecks
	In order to avoid silly mistakes
	As a teser
	I want to be able to check webelements properties

@WebElementsChecks
Scenario: Check Color of Webelement
	When I navigate to the page WebElementsColor
	Then The color of the element element should be red

@WebElementsChecks
Scenario: Check that element dissapears
	When I navigate to the page WebElementsColor
	Then The color of the element element should be red

@WebElementsChecks
Scenario: Check that element exists
	When I navigate to the page WebElementsColor
	Then The color of the element element should be red
	
@WebElementsChecks
Scenario: Check the element line number
	When I navigate to the page RegistrationUsersUrl
	Then The element has '2' lines