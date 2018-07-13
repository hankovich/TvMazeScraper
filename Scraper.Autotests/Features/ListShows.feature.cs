//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ListShows.feature.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ListShows.feature.cs type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Scraper.Autotests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ListShows")]
    public partial class ListShowsFeature
    {
        
        private ITestRunner testRunner;
        
#line 1 "ListShows.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ListShows", null, ProgrammingLanguage.CSharp, (string[])null);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Get existing data")]
        public virtual void GetExistingData()
        {
            ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get existing data", (string[])null);
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
 testRunner.Given("pageNumber is set to 1", (string)null, (TechTalk.SpecFlow.Table)null, "Given ");
#line 5
 testRunner.And("pageSize is set to 25", (string)null, (TechTalk.SpecFlow.Table)null, "And ");
#line 6
 testRunner.When("request send to server", (string)null, (TechTalk.SpecFlow.Table)null, "When ");
#line 7
 testRunner.Then("response contains 200 status code", (string)null, (TechTalk.SpecFlow.Table)null, "Then ");
#line 8
 testRunner.And("Shows is not empty", (string)null, (TechTalk.SpecFlow.Table)null, "And ");
#line 9
 testRunner.And("Errors is empty", (string)null, (TechTalk.SpecFlow.Table)null, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validation error")]
        public virtual void ValidationError()
        {
            ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validation error", (string[])null);
#line 11
this.ScenarioSetup(scenarioInfo);
#line 12
 testRunner.Given("pageNumber is set to -9", (string)null, (TechTalk.SpecFlow.Table)null, "Given ");
#line 13
 testRunner.And("pageSize is set to -1", (string)null, (TechTalk.SpecFlow.Table)null, "And ");
#line 14
 testRunner.When("request send to server", (string)null, (TechTalk.SpecFlow.Table)null, "When ");
#line 15
 testRunner.Then("response contains 400 status code", (string)null, (TechTalk.SpecFlow.Table)null, "Then ");
#line 16
 testRunner.And("Shows is empty", (string)null, (TechTalk.SpecFlow.Table)null, "And ");
#line 17
 testRunner.And("Errors is not empty", (string)null, (TechTalk.SpecFlow.Table)null, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
