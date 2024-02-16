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

        [Given(@"the user is on the Study Groups listing page")]
        public void GivenTheUserIsOnTheStudyGroupsListingPage()
        {
            _scenarioContext.Pending();
        }

        [When(@"the user selects a subject from the filter options")]
        public void WhenTheUserSelectsASubjectFromTheFilterOptions()
        {
            _scenarioContext.Pending();
        }

        [When(@"the user applies the filter")]
        public void WhenTheUserAppliesTheFilter()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the list should be updated to show only study groups of that subject")]
        public void ThenTheListShouldBeUpdatedToShowOnlyStudyGroupsOfThatSubject()
        {
            _scenarioContext.Pending();
        }

    }
}
