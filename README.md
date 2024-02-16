# Web Application Study Group Feature Testing

## Tópicos
- [Overview](#overview)
- [Test Strategy](#test-strategy)
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

## Acceptance Criteria

The project's success hinges on fulfilling key user capabilities:

1. Creating a single study group per subject.
2. Joining multiple study groups for different subjects.
3. Browsing and filtering existing study groups.
4. Exiting study groups as needed.

## Getting Started

For instructions on setting up the testing environment, executing tests, and contributing to this project, please refer to the README.md file.
