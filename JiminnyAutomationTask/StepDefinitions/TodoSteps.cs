using JiminnyAutomationTask.Hooks;
using JiminnyAutomationTask.Pages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiminnyAutomationTask.StepDefinitions
{
    [Binding]
    public class TodoSteps : TodoPage
    {
        //As it was a Single Page Application I directly decided to inherit the TodoPage, not creating a new instance of it.
        protected IWebDriver driver;
        protected WebDriverWait wait;
        //constructor was needed here because of the inheritance
        public TodoSteps(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        public List<string> expectedList = new List<string>();
        #region Given Section
        [Given(@"user navigates to todo page")]        
        public void GivenUserNavigatesToTodoPage()
        {
           NavigateToBaseURL();
        }
        #endregion
        #region When Section
        [When(@"user creates (.*) tasks named (.*)")]
        public void WhenUserCreatesNumberOfTasks(int numberOf, string taskName)
        {
            for (int i = 0; i < numberOf; i++)
            {
                CreateNewTask(taskName);
                PressEnter();
            }            
        }

        [When(@"user edits (.*) and renames them to (.*)")]
        public void WhenUserEditsNumberOfTasksNamedEditName(int numberOf, string editName)
        {
           EditExistingTask(editName);
           PressEnter();          
        }

        [When(@"user checks/unchecks (.*) tasks")]
        public void WhenUserCompletesTasks(int numberOf)
        {
            CheckOrUncheckTask(numberOf);
        }

        [When(@"user checks/unchecks all tasks with mass operation")]
        public void WhenUserChecksUnchecksAllTasksWithMassOperation()
        {
            CheckOrUncheckTaskMassOperation();
        }
        [When(@"user clicks Clear completed button")]
        public void WhenUserClicksClearCompletedButton()
        {
            ClearCompletedButtonClick();
        }

        [When(@"user navigates to (.*) section")]
        public void WhenUserNavigatesToSection(string sectionName)
        {
            SectionLinkTextName(sectionName).Click();
        }

        [When(@"user clicks remove for the (.*) tasks")]
        public void WhenUserClicksRemoveForTheTasks(int someTasks)
        {
            RemoveSomeTasks(someTasks);
        }

        #endregion




        #region Then Section
        [Then(@"tasks are created successfully")]
        public void ThenATaskIsCreatedSuccessfully()
        {
            List<string> tasks = new List<string>();
            AssertTaskNames();
            if(ResultList.Count == 1)
            {

                tasks.Add("task1");
                Assert.AreEqual(ResultList, tasks);
            }
            else
            {
                for(int i = 0; i < ResultList.Count; i++)
                {
                    tasks.Add("task3");                    
                }
                Assert.AreEqual(ResultList, tasks);
            }
            CompareTasksCount();
            Assert.AreEqual(TasksCountBeforeCreation + 1, TasksCountAfterCreation );
        }

        [Then(@"tasks are edited successfully")]
        public void ThenATaskIsEditedSuccessfully()
        {
            List<string> tasks = new List<string>();
            AssertTaskNames();
            if (ResultList.Count == 1)
            {

                tasks.Add("task1-edit");
                Assert.AreEqual(ResultList, tasks);
            }
            else
            {
                for (int i = 0; i < ResultList.Count; i++)
                {
                    tasks.Add("task3-edit");
                }
                Assert.AreEqual(ResultList, tasks);
            }
        }

        [Then(@"tasks (.*) are completed successfully")]
        public void ThenATaskIsCompletedSuccessfully(int some)
        {
            AssertTaskCompletedOrUnchecked(some);
            for(int i = 0; i < some; i++)
            {
                expectedList.Add("todo completed");
                expectedList.Add("line-through solid rgb");
            }
            Assert.AreEqual(expectedList, ResultList);
            expectedList.Clear();
        }

        [Then(@"tasks (.*) are unchecked successfully")]
        public void ThenTasksAreUncheckedSuccessfully(int some)
        {
            AssertTaskCompletedOrUnchecked(some);            
            for (int i = 0; i < some; i++)
            {
                expectedList.Add("todo");
                expectedList.Add("none solid rgb");
            }
            Assert.AreEqual(expectedList, ResultList);
            expectedList.Clear();
        }
        [Then(@"completed (.*) tasks out of (.*) tasks cleared successfully")]
        public void ThenCompletedTasksClearedSuccessfully(int some, int numberOf)
        {
            Assert.AreEqual(AssertCompletedTasksCleared(), numberOf - some);
        }
        
        [Then(@"counter shows only the difference between all (.*) and (.*) completed tasks")]
        public void ThenCounterShowsOnlyTheDifferenceBetweenAllAndCompletedTasks(int numberOf, int some)
        {
            int difference = numberOf - some;
            expectedList.Add(difference.ToString()+" items left"); 
            
            Assert.AreEqual(difference, AssertUncheckedTasksCounter());
            //After I created the 2nd assertion the 1st became a bit useless but still I decided to keep it.
            Assert.AreEqual(expectedList, ResultList);
        }

        [Then(@"Active/Completed section should contain all (.*) tasks minus the completed/unchecked (.*) tasks")]
        public void ThenActiveOrCompletedSectionShouldContainAllTasksMinusTheCompletedTasks(int allTasks, int someTasks)
        {
            Assert.AreEqual(allTasks- someTasks ,AllTasks.Count());
        }
      

        #endregion

    }
}
