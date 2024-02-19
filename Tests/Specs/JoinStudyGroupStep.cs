using Gherkin.Ast; // Importing Gherkin AST (Abstract Syntax Tree) for working with Gherkin syntax
using Gherkin.CucumberMessages.Types; // Importing Gherkin CucumberMessages Types for working with Gherkin messages
using StudyGroupsManager.Models; // Importing StudyGroupsManager Models for accessing study group related models
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow; // Importing TechTalk SpecFlow for writing BDD-style tests

namespace StudyGroupsManager.Tests.Specs
{
    [Binding]
    public class JoinStudyGroupSteps
    {
        private readonly ScenarioContext _scenarioContext; // ScenarioContext for sharing data between steps

        public JoinStudyGroupSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        // Step definitions with translations

        [Given(@"the user is logged in and on the Study Groups listing page")]
        public void GivenTheUserIsLoggedInAndOnTheStudyGroupsListingPage()
        {
            _scenarioContext.Pending(); // Marking step as pending, to be implemented
        }

        [When(@"the user selects a study group to join")]
        public void WhenTheUserSelectsAStudyGroupToJoin()
        {
            _scenarioContext.Pending(); // Marking step as pending, to be implemented
        }

        [When(@"the user confirms the action")]
        public void WhenTheUserConfirmsTheAction()
        {
            _scenarioContext.Pending(); // Marking step as pending, to be implemented
        }

        [Then(@"the user should be added to the selected study group")]
        public void ThenTheUserShouldBeAddedToTheSelectedStudyGroup()
        {
            _scenarioContext.Pending(); // Marking step as pending, to be implemented
        }

        [Then(@"the user should see a confirmation message")]
        public void ThenTheUserShouldSeeAConfirmationMessage()
        {
            _scenarioContext.Pending(); // Marking step as pending, to be implemented
        }

    }
}
