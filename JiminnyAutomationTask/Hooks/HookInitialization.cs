using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System.Reflection;

namespace JiminnyAutomationTask.Hooks
{
    [Binding]
    public class HookInitialization
    {
        private IWebDriver driver;

        private readonly IObjectContainer container;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string ReportPath;
        public HookInitialization(IObjectContainer container)
        {
            this.container = container;
        }
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string path = path1 + "TodoReport\\index.html";
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);            
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        [BeforeFeature]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }


        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "When")
                                scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "Then")
                                scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "And")
                                scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if(ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "Then") {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
            }
        }

        [BeforeScenario]
        public void FirstBeforeScenario()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
           
            driver.Manage().Window.Maximize(); 
            container.RegisterInstanceAs(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            IWebDriver driver = container.Resolve<IWebDriver>();
            driver.Quit();
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            //Flush report once test completes
            extent.Flush();
            
        }
    }
}