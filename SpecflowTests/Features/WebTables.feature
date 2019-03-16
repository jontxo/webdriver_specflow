Feature: WebTables
	In order to be able to manipulate data in tables
	As an application tester
	I want to be able to do easily the main operations in tables

@webtables
Scenario Outline: Compare webTable cell value with expected result
	Given I navigate to the page webtables
	Then at the webtable <webtable> should appear the value <result> at the structure <structure> in the column <column>
		
	Examples: 
	| webtable | structure  | column | result |
	| webTable | Taipei 101 | Height | 509m   |

@webtables
Scenario Outline: Check if a column in a table has a value in any of his cells
	Given I navigate to the page webtables
	Then at the webtable <webtable> all the cells of the column <column>,  contain the value <value>

	Examples:
	| webtable | column | value |
	| webTable | 3      | m     |