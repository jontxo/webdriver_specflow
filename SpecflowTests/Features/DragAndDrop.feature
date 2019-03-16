Feature: DragAndDrop
	In order to use the web apps more quickly
	As a computer user
	I want to be able to drag & drop elements

@DragAndDrop
Scenario Outline: Drag and drop an element over another element
	Given I navigate to the page DragAndDropUrl
	When I drag webElement <element1> and drop over webElement <element2>
	Then the result should be that the first element is moved near the second element
	Examples:
	| element1     | element2 |
	| highTatras   | trash    |
	| highTatras2  | trash    |

@DragAndDrop
Scenario: Drag and drop an element to a position
	Given I navigate to the page DragAndDropUrl
	When I drag webElement highTatras and drop ot the coordinates: x: 500, y: 0
	Then the result should be that the element has changed his position

