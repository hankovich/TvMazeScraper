Feature: ListShows

Scenario: Get existing data
	Given pageNumber is set to 1
	And pageSize is set to 25
	When request send to server
	Then response contains 200 status code
	And Shows is not empty
	And Errors is empty

Scenario: Validation error
	Given pageNumber is set to -9
	And pageSize is set to -1
	When request send to server
	Then response contains 400 status code
	And Shows is empty
	And Errors is not empty
