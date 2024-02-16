Feature: Exiting a Study Group
  As a user
  I want to exit a study group
  So that I can leave groups that I am no longer interested in

  @ignore
  Scenario: Exiting a study group
    Given the user is a member of a study group and on the Study Group details page
    When the user clicks the exit group button
    And the user confirms the action
    Then the user should be removed from the study group
    And the user should see a confirmation message