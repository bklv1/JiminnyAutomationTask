

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;

namespace JiminnyAutomationTask.Pages
{
    [Binding]
    public partial class TodoPage : BaseClass
    {
        private IWebDriver driver;
        private WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        //when combined explicit wait with ExpectedConditions class, the workflow becomes really fluent to work with.
        //Better than FluentWait() in my opinion.

        Actions actions;
        public string BaseUrl => "https://todomvc.com/examples/vue/";
        public int TasksCountBeforeCreation { get; set; }
        public int TasksCountAfterCreation { get; set; }
        public List<string> ResultList = new List<string>();
        public TodoPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        //created the virtual navigate method as I may decide to override it somewhere later on.
        public virtual void NavigateToBaseURL()
        {
            driver.Navigate().GoToUrl(this.BaseUrl);
        }
        public void CreateNewTask(string taskName)
        {
            //Before I create a new task I need to make sure if we have none, or we have some tasks created.
            if (MainSection.GetAttribute("style").Equals("display: none;"))
            {
                TasksCountBeforeCreation = 0;
            }
            else
            {
                TasksCountBeforeCreation = AllTasks.Count();
            }

            TaskTextBox.SendKeys(taskName);
        }
        public void EditExistingTask(string editName)
        {
            actions = new Actions(driver);
            foreach (var task in AllTasks)
            {
                actions.MoveToElement(task).Build().Perform();
                actions.DoubleClick(task).SendKeys(editName).Build().Perform();

            }
        }
        public void PressEnter()
        {
            TaskTextBox.SendKeys(Keys.Enter);
        }
        public void CompareTasksCount()
        {
            TasksCountAfterCreation = AllTasks.Count();
        }
        public void AssertTaskNames()
        {
            foreach (var task in AllTasks)
            {
                ResultList.Add(task.Text);
            }
        }
        public void CheckOrUncheckTask(int some)
        {
            for (int i = 0; i < some; i++)
            {
                AllCheckboxes[i].Click();
            }
        }  
        public void RemoveSomeTasks(int some)
        {
            actions = new Actions(driver);
            for (int i = 0; i < some; i++)
            {
                actions.MoveToElement(AllRemoveButtons[i]).Build().Perform();
                actions.Click(AllRemoveButtons[i]).Build().Perform();
            }
        }
        public void CheckOrUncheckTaskMassOperation()
        {
             ToggleAllMassOperation.Click();
        }
        public void ClearCompletedButtonClick()
        {
            ClearCompletedButton.Click();
        }
        public void AssertTaskCompletedOrUnchecked(int some)
        {
            //The way I assert tasks are completed is by taking their class attribute.
            //For example, when it is unchecked it is with class name "todo",
            //but when I complete it its class name becomes "todo completed"
            //also the css text-decoration value must be "line-through" when checked
            //and the css text-decoration value must be "none" when unchecked
            ResultList.Clear();
            for (int i = 0; i < some; i++)
            {
                ResultList.Add(AllLiElements[i].GetAttribute("class"));
                string cssValue = AllTasks[i].GetCssValue("text-decoration");
                int index = cssValue.IndexOf("(");
                string result = cssValue.Substring(0, index);
                ResultList.Add(result);
            }

        }
        public int AssertCompletedTasksCleared()
        {
            int result=  AllTasks.Count();
            return result;
        }
        public int AssertUncheckedTasksCounter()
        {
            //from the Strong HTML element I take the count as string and convert it to int.
            int result =int.Parse(UncheckedItemCounterElement.Text);
            //also I store the rest of the message "items left" in the assert List<string>
            ResultList.Clear();
            ResultList.Add(ItemsLeftMessage.Text);
            return result;
        }
        public IWebElement SectionLinkTextName(string linkText)
        {
            IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.LinkText(linkText)));
            return element;
        }
    }
}