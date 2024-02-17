# Web Application Study Group Feature Testing

## Topics
- [Overview](#overview)
- [Acceptance Criteria](#acceptance-criteria)
- [Test Strategy](#test-strategy)
- [Test Cases](#test-cases)
- [Automated Tests Implementation](#automated-tests-implementation)
- [Executing Tests](#executing-tests)
- [SQL Query for StudyGroups Retrieval](#sql-query-for-studygroups-retrieval)
- [Getting Started](#getting-started)

## Overview

The study group feature introduces a new entity, `StudyGroups`, into the application's MS SQL database, expanding the existing data model to include subjects and users. This testing suite covers the full spectrum of testing requirements:

- **Unit Testing**: Validates the internal logic of the `StudyGroups` entity and related business rules.
- **Component Testing**: Ensures the API controller interacts correctly with the repository, supporting operations like group creation, membership management, and data retrieval.
- **E2E Testing (Manual)**: Focuses on the user interface and real-world use cases, confirming that users can effortlessly navigate the feature to create and participate in study groups.

## Acceptance Criteria

The project's success hinges on fulfilling key user capabilities:

1. Creating a single study group per subject.
2. Joining multiple study groups for different subjects.
3. Browsing and filtering existing study groups.
4. Exiting study groups as needed.

## Test Strategy

- **Test Case Identification**: Based on the acceptance criteria, test cases encompass functionality checks for group creation, user addition/removal, and data integrity.
- **Automated Tests**: Written using the NUnit framework, automated tests cover both unit and component levels, emphasizing robustness and regression readiness.
- **SQL Query Validation**: A specific SQL query retrieves all study groups with at least one user whose name starts with 'M', sorted by creation date, demonstrating backend data handling.
- **Documentation and Reporting**: Comprehensive documentation in Word/Excel formats, coupled with the use of GitLab/GitHub for code sharing and review, facilitates transparency and collaboration.

## Test Cases

### Study Group Creation
- **Steps**: Attempt to create a group with a valid name, select a subject, and verify creation.
- **Inputs**: Group name (5-30 characters), subject (Math, Chemistry, Physics).
- **Level**: Unit for validation logic, Component for repository interaction, E2E for UI process.
- **Regression**: Yes.

### Joining an Existing Group
- **Steps**: User attempts to join an existing group.
- **Inputs**: User ID, study group ID.
- **Level**: Component, Manual E2E to check the UI.
- **Regression**: Yes.

### Viewing and Filtering Groups
- **Steps**: User views all groups and applies a filter by subject.
- **Inputs**: Subject for filtering.
- **Level**: Manual E2E.
- **Regression**: Optional.

### Exiting a Study Group
- **Steps**: User exits a study group.
- **Inputs**: User ID, study group ID.
- **Level**: Component, Manual E2E.
- **Regression**: Yes.

## Gherkin to Manual Test Process
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

Feature: Viewing and Filtering Groups
  As a user
  I want to filter study groups by subject
  So that I can find relevant groups more easily

  @ignore
  Scenario: Filtering study groups by a specific subject
    Given the user is on the Study Groups listing page
    When the user selects a subject from the filter options
    And the user applies the filter
    Then the list should be updated to show only study groups of that subject

## Automated Tests Implementation

To write the automated tests described, we use NUnit to create unit and component tests verifying the business logic of the application and the API integration, respectively. Manual E2E tests are documented separately, detailing steps for direct interaction with the user interface.

### Unit Tests for StudyGroup Class

Unit tests verify the addition and removal of users, as well as the validation of the group's name and subject. Each unit test focuses on a specific aspect of the StudyGroup functionality, using assertions to validate expected outcomes.

### Component Tests for StudyGroupController

Component tests examine the integration between the API methods and the repository, such as group creation, joining, viewing, and leaving groups. These tests ensure that the StudyGroupController interacts correctly with the underlying database through the repository.

Each automated test focuses on a specific aspect of the functionality, utilizing assertions to validate the expected results. For specific examples of implementing these tests within the context of your application, we recommend consulting the NUnit documentation.

## Executing Tests

### Executing Tests in Visual Studio

1. Open the Solution: Start Visual Studio and open the project's solution file (*.sln) that contains the tests.
2. Build the Solution: Build the solution to ensure all projects are up-to-date and there are no compilation errors. This can be done by selecting Build > Build Solution from the menu bar.
3. Test Explorer: Open the Test Explorer in Visual Studio by navigating to Test > Windows > Test Explorer.
4. Run Tests: In the Test Explorer, you will see a list of all unit tests in the project. You can run all tests by clicking Run All, or execute specific tests by right-clicking on the desired test and selecting Run.
5. View Results: After the tests have been run, you can view the results in the Test Explorer, including which tests passed and which failed.

### Executing Tests in Visual Studio Code

1. Install .NET Core Test Explorer: Open Visual Studio Code, go to the extensions section, and search for .NET Core Test Explorer. Install the extension.
2. Open the Project: Open the project folder in Visual Studio Code that contains the tests.
3. Build the Project: Open the integrated terminal in VS Code (Ctrl+) and execute the command dotnet build` to build the project.
4. Run Tests: With the .NET Core Test Explorer extension installed and the project built, you should see your tests listed in the Test Explorer sidebar. You can run all tests by clicking the play icon, or run individual tests by right-clicking on the desired test and selecting Run test.
5. View Results: The test results will be displayed directly in the Test Explorer of VS Code.

### Executing Tests in Visual Studio Code (Using the Integrated Terminal)

1. Open the Integrated Terminal: In Visual Studio Code, you can open the integrated terminal with the shortcut Ctrl+`.
2. Navigate to the Project Directory: Similar to the command prompt, use the cd command to navigate to the directory containing your test project.
3. Execute the Tests: Use the dotnet test command to run the tests.

## SQL Query for StudyGroups Retrieval

To retrieve all StudyGroups with at least one user whose name starts with 'M' and sort them by creation date, the following SQL query can be used:

``sql
SELECT DISTINCT sg.* FROM StudyGroups sg
JOIN Users u ON sg.StudyGroupId = u.StudyGroupId
WHERE u.Name LIKE 'M%'
ORDER BY sg.CreateDate;

## Build and Test CI with GitHub Actions

To build and execute tests, we can use the GitHub Actions pipeline. Follow the steps below or make new commits to trigger automatic pipeline execution.

1. Access the StudyGroupsManager repository.
2. Click on the "Actions" tab.
3. In the "Workflows" section, click on the "StudyGroupsManager Build and Test" workflow.
4. Click on "Run workflow".
5. Select the "main" branch.
6. Click on "Run workflow".
7. At the end of the process, we will have a log containing all the build and test processes for the project.

## Getting Started

For instructions on setting up the testing environment, executing tests, and contributing to this project, please refer to the README.md file.
