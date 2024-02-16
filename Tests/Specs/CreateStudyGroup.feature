Feature: Study Group Creation
  As a user
  I want to create a study group
  So that I can collaborate with others on the same subject
  
  Scenario: Creating a study group with a valid name and subject
    Given the user is on the Create Study Group page
    When the user enters a group name between 5-30 characters and selects a valid subject
    And the user submits the form
    Then a new study group should be created
    And the user should be redirected to the Study Group details page