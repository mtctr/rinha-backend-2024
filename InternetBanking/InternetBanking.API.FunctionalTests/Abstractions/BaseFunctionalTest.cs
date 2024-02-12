namespace InternetBanking.API.FunctionalTests.Abstractions
{
    public class BaseFunctionalTest : IClassFixture<FunctionalTestsWebAppFactory>
    {
        public BaseFunctionalTest(FunctionalTestsWebAppFactory factory) 
        {
            HttpClient = factory.CreateClient();
        }

        protected HttpClient HttpClient { get; init; }
    }
}
