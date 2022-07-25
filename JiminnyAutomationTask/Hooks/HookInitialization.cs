using System.Reflection;

namespace JiminnyAutomationTask.Hooks
{
    [Binding]
    public class HookInitialization
    {
        private IWebDriver driver;

        private readonly IObjectContainer container;

        public HookInitialization(IObjectContainer container)
        {
            this.container = container;
        }

        [BeforeScenario]
        public void FirstBeforeScenario()
        {
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
    }
}