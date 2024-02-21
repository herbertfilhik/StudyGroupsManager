Feature: Viewing and Filtering Groups
  As a user
  I want to filter study groups by subject
  So that I can find relevant groups more easily

  Scenario: Filtering study groups by a specific subject
    Given the user is on the Study Groups listing page
    When the user selects a subject from the filter options
    And the user applies the filter
    Then the list should be updated to show only study groups of that subject