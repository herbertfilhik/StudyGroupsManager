# Web Application Study Group Feature Testing

## Topics
- [Overview](#overview)
- [Test Strategy](#test-strategy)
- [Test Cases](#test-cases)
- [Automated Tests Implementation](#automated-tests-implementation)
- [SQL Query for StudyGroups Retrieval](#sql-query-for-studygroups-retrieval)
- [Acceptance Criteria](#acceptance-criteria)
- [Getting Started](#getting-started)

## Overview

The study group feature introduces a new entity, `StudyGroups`, into the application's MS SQL database, expanding the existing data model to include subjects and users. This testing suite covers the full spectrum of testing requirements:

- **Unit Testing**: Validates the internal logic of the `StudyGroups` entity and related business rules.
- **Component Testing**: Ensures the API controller interacts correctly with the repository, supporting operations like group creation, membership management, and data retrieval.
- **E2E Testing (Manual)**: Focuses on the user interface and real-world use cases, confirming that users can effortlessly navigate the feature to create and participate in study groups.

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

## Automated Tests Implementation

To write the automated tests described, we use NUnit to create unit and component tests verifying the business logic of the application and the API integration, respectively. Manual E2E tests are documented separately, detailing steps for direct interaction with the user interface.

### Unit Tests for StudyGroup Class

Unit tests verify the addition and removal of users, as well as the validation of the group's name and subject. Each unit test focuses on a specific aspect of the StudyGroup functionality, using assertions to validate expected outcomes.

### Component Tests for StudyGroupController

Component tests examine the integration between the API methods and the repository, such as group creation, joining, viewing, and leaving groups. These tests ensure that the StudyGroupController interacts correctly with the underlying database through the repository.

Each automated test focuses on a specific aspect of the functionality, utilizing assertions to validate the expected results. For specific examples of implementing these tests within the context of your application, we recommend consulting the NUnit documentation.

## SQL Query for StudyGroups Retrieval

To retrieve all StudyGroups with at least one user whose name starts with 'M' and sort them by creation date, the following SQL query can be used:

``sql
SELECT DISTINCT sg.* FROM StudyGroups sg
JOIN Users u ON sg.StudyGroupId = u.StudyGroupId
WHERE u.Name LIKE 'M%'
ORDER BY sg.CreateDate;

## Acceptance Criteria

The project's success hinges on fulfilling key user capabilities:

1. Creating a single study group per subject.
2. Joining multiple study groups for different subjects.
3. Browsing and filtering existing study groups.
4. Exiting study groups as needed.

## Getting Started

For instructions on setting up the testing environment, executing tests, and contributing to this project, please refer to the README.md file.
