using Gherkin.Ast; // Importing Gherkin AST for defining Gherkin syntax
using StudyGroupsManager.Models; // Importing StudyGroupsManager.Models namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow; // Importing TechTalk SpecFlow for defining BDD-style tests

namespace StudyGroupsManager.Tests.Specs
{
    [Binding]
    public class StudyGroupSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public StudyGroupSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        // Step definition for given step
        [Given(@"the user is a member of a study group and on the Study Group details page")]
        public void GivenTheUserIsAMemberOfAStudyGroupAndOnTheStudyGroupDetailsPage()
        {
            _scenarioContext.Pending(); // Indicates that this step is pending implementation
        }

        // Step definition for when step
        [When(@"the user clicks the exit group button")]
        public void WhenTheUserClicksTheExitGroupButton()
        {
            _scenarioContext.Pending(); // Indicates that this step is pending implementation
        }

        // Step definition for when step
        [When(@"the user confirms the action")]
        public void WhenTheUserConfirmsTheAction()
        {
            _scenarioContext.Pending(); // Indicates that this step is pending implementation
        }

        // Step definition for then step
        [Then(@"the user should be removed from the study group")]
        public void ThenTheUserShouldBeRemovedFromTheStudyGroup()
        {
            _scenarioContext.Pending(); // Indicates that this step is pending implementation
        }

        // Step definition for then step
        [Then(@"the user should see a confirmation message")]
        public void ThenTheUserShouldSeeAConfirmationMessage()
        {
            _scenarioContext.Pending(); // Indicates that this step is pending implementation
        }

    }
}
