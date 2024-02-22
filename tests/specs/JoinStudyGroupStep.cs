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
