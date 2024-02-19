using TechTalk.SpecFlow;

namespace StudyGroupsManager.Tests.Specs
{
    [Binding]
    public class ViewAndFilterStudyGroupsStep
    {
        private readonly ScenarioContext _scenarioContext;

        public ViewAndFilterStudyGroupsStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        // Given the user is on the Study Groups listing page
        [Given(@"the user is on the Study Groups listing page")]
        public void GivenTheUserIsOnTheStudyGroupsListingPage()
        {
            // This step is pending implementation
            _scenarioContext.Pending();
        }

        // When the user selects a subject from the filter options
        [When(@"the user selects a subject from the filter options")]
        public void WhenTheUserSelectsASubjectFromTheFilterOptions()
        {
            // This step is pending implementation
            _scenarioContext.Pending();
        }

        // When the user applies the filter
        [When(@"the user applies the filter")]
        public void WhenTheUserAppliesTheFilter()
        {
            // This step is pending implementation
            _scenarioContext.Pending();
        }

        // Then the list should be updated to show only study groups of that subject
        [Then(@"the list should be updated to show only study groups of that subject")]
        public void ThenTheListShouldBeUpdatedToShowOnlyStudyGroupsOfThatSubject()
        {
            // This step is pending implementation
            _scenarioContext.Pending();
        }

    }
}
