using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiminnyAutomationTask
{
    public class BaseClass : TechTalk.SpecFlow.Steps
    {
        protected IWebDriver driver;
        public BaseClass(IWebDriver driver)
        {
            this.driver = driver;
         
        }

    }
}
