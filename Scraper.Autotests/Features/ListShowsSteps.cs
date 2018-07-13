//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ListShowsSteps.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ListShowsSteps type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Autotests.Features
{
    using System.Collections;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using Scraper.Autotests.Models;

    using TechTalk.SpecFlow;

    [Binding]
    public class ListShowsSteps
    {
        [Given(@"(.*) is set to (.*)")]
        public void GivenPageNumberIsSetTo(string queryParameterName, string queryParameterValue)
        {
            ListShowsContext context = GetContext();

            context.QueryParameters[queryParameterName] = queryParameterValue;

            ScenarioContext.Current[nameof(ListShowsContext)] = context;
        }

        [When(@"request send to server")]
        public async Task WhenRequestSendToServer()
        {
            ListShowsContext context = GetContext();

            using (var client = new HttpClient())
            {
                string listShowsBaseUri = ConfigurationManager.AppSettings["listShowsBaseUri"];

                string queryString = string.Join("&", context.QueryParameters.Select(par => $"{par.Key}={par.Value}"));

                string listShowsUri;

                if (string.IsNullOrEmpty(queryString))
                {
                    listShowsUri = listShowsBaseUri;
                }
                else
                {
                    listShowsUri = $"{listShowsBaseUri}?{queryString}";
                }

                context.ServerResponseMessage = await client.GetAsync(listShowsUri);

                string responseJson = await context.ServerResponseMessage.Content.ReadAsStringAsync();
                context.ResponsePage = JsonConvert.DeserializeObject<ShowsPage>(responseJson);
            }

            ScenarioContext.Current[nameof(ListShowsContext)] = context;
        }

        [Then(@"response contains (.*) status code")]
        public void ThenResponseContainsStatusCode(int code)
        {
            ListShowsContext context = GetContext();

            if (context.ServerResponseMessage == null)
            {
                Assert.Fail("Context is null");
            }

            Assert.AreEqual(code, (int)context.ServerResponseMessage.StatusCode);
        }

        [Then(@"(.*) is not empty")]
        public void ThenShowsIsNotEmpty(string responseProperty)
        {
            PropertyIsEmptyInternal(responseProperty, requiresEmpty: false);
        }

        [Then(@"(.*) is empty")]
        public void ThenErrorsIsEmpty(string responseProperty)
        {
            PropertyIsEmptyInternal(responseProperty, requiresEmpty: true);
        }

        private static void PropertyIsEmptyInternal(string responseProperty, bool requiresEmpty)
        {
            ListShowsContext context = GetContext();

            var propertyInfo = typeof(ShowsPage).GetProperty(responseProperty);

            if (propertyInfo == null)
            {
                Assert.Fail($"There is no such property: {responseProperty}");
            }

            var propertyValue = propertyInfo.GetValue(context.ResponsePage);

            if (propertyValue == null)
            {
                Assert.AreEqual(true, requiresEmpty);
                return;
            }

            switch (propertyValue)
            {
                case string str:
                    Assert.AreEqual(string.IsNullOrEmpty(str), requiresEmpty);
                    break;
                case IEnumerable enumerable:
                    Assert.AreEqual(!enumerable.Cast<object>().Any(), requiresEmpty);
                    break;
                default:
                    Assert.Fail($"{propertyValue.GetType().Name} is not supported as property type for checking");
                    break;
            }
        }

        private static ListShowsContext GetContext()
        {
            if (ScenarioContext.Current.TryGetValue(nameof(ListShowsContext), out ListShowsContext context))
            {
                return context;
            }

            return new ListShowsContext();
        }
    }
}
