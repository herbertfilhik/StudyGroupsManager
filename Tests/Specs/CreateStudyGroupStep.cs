
using TechTalk.SpecFlow;

namespace StudyGroupsManager.Tests.Specs
{    
    [Binding]
    public class CreateStudyGroupSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CreateStudyGroupSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the user is on the Create Study Group page")]
        public void GivenTheUserIsOnTheCreateStudyGroupPage()
        {
            // Implementação do código para verificar se o usuário está na página de criação de grupo de estudo
        }

        [When(@"the user enters a group name '(.*)' and selects a valid subject '(.*)'")]
        public void WhenTheUserEntersAGroupNameAndSelectsAValidSubject(string groupName, string subject)
        {
            // Implementação do código para simular o usuário inserindo um nome de grupo e selecionando um assunto
        }

        [When(@"the user submits the form")]
        public void WhenTheUserSubmitsTheForm()
        {
            // Implementação do código para simular a submissão do formulário de criação de grupo de estudo
        }

        [Then(@"a new study group should be created")]
        public void ThenANewStudyGroupShouldBeCreated()
        {
            // Implementação do código para verificar se um novo grupo de estudo foi criado
        }

        [Then(@"the user should be redirected to the Study Group details page")]
        public void ThenTheUserShouldBeRedirectedToTheStudyGroupDetailsPage()
        {
            // Implementação do código para verificar se o usuário foi redirecionado para a página de detalhes do grupo de estudo
        }
    }
}
