﻿Feature: Testing Todo list application
In my opinion the whole small application can be seen as one feature, because it has a single use
#I tried so hard the tests to be written in a simpler and smaller test cases,
#so the tests are expected fail only in one place (if they eventually fail)
#Some tests are data driven (parameterized) as I think this the more efficient approach
Scenario Outline: Adding todo tasks
Given user navigates to todo page
When user creates <numberOf> tasks named <taskName>
Then tasks with <taskName> are created successfully
Examples: 
| numberOf | taskName |
| 1        | task1    |
| 10       | task10   |


Scenario Outline: Edit todo tasks
Given user navigates to todo page
When user creates <numberOf> tasks named <taskName>
And user edits <numberOf> and renames them to <editName>
Then tasks are edited successfully
Examples: 
| numberOf | taskName | editName   |
| 1        | task1    | -edit	   |
| 3        | task3    | -edit      |


Scenario Outline: Edit already completed tasks
Given user navigates to todo page
When user creates <numberOf> tasks named <taskName>
And user checks/unchecks <numberOf> tasks
And user edits <numberOf> and renames them to <editName>
Then tasks are edited successfully
Examples: 
| numberOf | taskName | editName   |
| 1        | task1    | -edit	   |


Scenario: Complete tasks
Given user navigates to todo page
When user creates 3 tasks named task
And user checks/unchecks 3 tasks
Then tasks 2 are completed successfully


Scenario: Uncheck completed tasks
Given user navigates to todo page
When user creates 3 tasks named task
And user checks/unchecks 3 tasks
And user checks/unchecks 2 tasks
Then tasks 2 are unchecked successfully


Scenario: Complete all tasks with mass operation
Given user navigates to todo page
When user creates 2 tasks named task
And user checks/unchecks all tasks with mass operation
Then tasks 2 are completed successfully


Scenario: Uncheck all tasks with mass operation
Given user navigates to todo page
When user creates 2 tasks named task
And user checks/unchecks all tasks with mass operation
And user checks/unchecks all tasks with mass operation
Then tasks 2 are unchecked successfully


Scenario: "Clear completed" tasks button functionality
Given user navigates to todo page
When user creates 3 tasks named task
And user checks/unchecks 2 tasks
And user clicks Clear completed button
Then completed 2 tasks out of 3 tasks cleared successfully


Scenario: Unchecked tasks counter
Given user navigates to todo page
When user creates 3 tasks named task
And user checks/unchecks 1 tasks
Then counter shows only the difference between all 3 and 1 completed tasks


Scenario: "Active" section contains only the tasks from All section that are unchecked
Given user navigates to todo page
When user creates 5 tasks named tasks
And user checks/unchecks 2 tasks
And user navigates to Active section
Then Active/Completed section should contain all 5 tasks minus the completed/unchecked 2 tasks


Scenario: "Completed" section contains only the tasks from All section that are completed
Given user navigates to todo page
When user creates 5 tasks named tasks
And user checks/unchecks 2 tasks
* user navigates to Completed section
Then Active/Completed section should contain all 5 tasks minus the completed/unchecked 3 tasks


Scenario: Remove completed task from remove button
Given user navigates to todo page
When user creates 2 tasks named task
And user checks/unchecks 1 tasks
* user clicks remove for the 1 tasks
Then completed 1 tasks out of 2 tasks cleared successfully


Scenario: Remove unchecked task from remove button
Given user navigates to todo page
When user creates 2 tasks named task
* user clicks remove for the 1 tasks
Then completed 1 tasks out of 2 tasks cleared successfully


Scenario: Toggle All Arrow disappears when there are no tasks
Given user navigates to todo page
When user creates 1 tasks named todo today
Then toggle-all arrow is present
When user clicks remove for the 1 tasks
Then toggle-all arrow is not present


Scenario: Footer disappears when there are no tasks
Given user navigates to todo page
When user creates 1 tasks named todo today
Then footer is present
When user clicks remove for the 1 tasks
Then footer is not present


Scenario: When user completes task from Active section it goes to Complete
Given user navigates to todo page
When user creates 2 tasks named active to complete
And user navigates to Active section
And user checks/unchecks 1 tasks
When user navigates to Completed section
Then the 1 completed of 2 tasks goes from Active to Completed


Scenario: When all tasks are unchecked the "Clear Completed" button disappears
Given user navigates to todo page
When user creates 1 tasks named todo today
And user checks/unchecks 1 tasks
Then Clear Completed button is present
When user checks/unchecks 1 tasks
Then Clear Completed button is not present


Scenario: "What needs to be done?" placeholder must be present
Given user navigates to todo page
Then placeholder value must say What needs to be done?

Scenario: Todos heading must be present 
Given user navigates to todo page
Then heading value must be todos

Scenario: Double-click to edit a todo tip must be present
Given user navigates to todo page
Then the tip is present