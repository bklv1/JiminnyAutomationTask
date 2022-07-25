using SeleniumExtras.WaitHelpers;

namespace JiminnyAutomationTask.Pages
{ 
    public partial class TodoPage
    {
        public IWebElement TaskTextBox => wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("new-todo")));
        public IWebElement MainSection => wait.Until(ExpectedConditions.ElementExists(By.ClassName("main")));
        public IWebElement ToggleAllMassOperation => wait.Until(ExpectedConditions.ElementExists(By.XPath("//label[@for=\"toggle-all\"]")));
        public IWebElement ItemsLeftMessage => wait.Until(ExpectedConditions.ElementExists(By.ClassName("todo-count")));
        public IWebElement RemoveButton => wait.Until(ExpectedConditions.ElementExists(By.ClassName("destroy")));
        public IWebElement UncheckedItemCounterElement => wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[@class=\"todo-count\"]/strong")));
        public IWebElement ClearCompletedButton => wait.Until(ExpectedConditions.ElementExists(By.ClassName("clear-completed")));
        public List<IWebElement> AllTasks => wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//ul[@class=\"todo-list\"]//label"))).ToList();
        public List<IWebElement> AllCheckboxes => wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//ul[@class=\"todo-list\"]//input[@type=\"checkbox\"]"))).ToList();
        public List<IWebElement> AllLiElements => wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//ul[@class=\"todo-list\"]/li"))).ToList();
        public List<IWebElement> AllRemoveButtons => wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("destroy"))).ToList();




    }
}