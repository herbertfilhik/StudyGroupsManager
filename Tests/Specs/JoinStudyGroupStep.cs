using Gherkin.Ast;
using Gherkin.CucumberMessages.Types;
using StudyGroupsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace StudyGroupsManager.Tests.Specs
{
    [Binding]
    public class JoinStudyGroupSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public JoinStudyGroupSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the user is logged in and on the Study Groups listing page")]
        public void GivenTheUserIsLoggedInAndOnTheStudyGroupsListingPage()
        {
            _scenarioContext.Pending();
        }

        [When(@"the user selects a study group to join")]
        public void WhenTheUserSelectsAStudyGroupToJoin()
        {
            _scenarioContext.Pending();
        }

        [When(@"the user confirms the action")]
        public void WhenTheUserConfirmsTheAction()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the user should be added to the selected study group")]
        public void ThenTheUserShouldBeAddedToTheSelectedStudyGroup()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the user should see a confirmation message")]
        public void ThenTheUserShouldSeeAConfirmationMessage()
        {
            _scenarioContext.Pending();
        }

    }
}
