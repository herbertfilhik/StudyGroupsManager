Feature: Joining an Existing Group
  As a user
  I want to join an existing study group
  So that I can participate in group learning activities

  @ignore
  Scenario: Joining an existing study group with valid credentials
    Given the user is logged in and on the Study Groups listing page
    When the user selects a study group to join
    And the user confirms the action
    Then the user should be added to the selected study group
    And the user should see a confirmation message