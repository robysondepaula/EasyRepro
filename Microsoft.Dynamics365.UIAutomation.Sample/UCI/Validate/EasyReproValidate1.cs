// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V111.CSS;
using Microsoft.Dynamics365.UIAutomation.Sample.Web;
using System.IO;


namespace Microsoft.Dynamics365.UIAutomation.Sample.UCI
{
    [TestClass]
    public class EasyReproValidate1 : TestsBase
    {
        private IWebDriver driver;
        [TestMethod]
        public void UCITestConfirmFieldIsNotRequired()
        {

            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);

                xrmApp.Navigation.OpenApp(UCIAppName.CustomerService);

                xrmApp.Navigation.OpenSubArea("CustomerService", "Accounts");

                xrmApp.Grid.Search("test1");

                xrmApp.Grid.OpenRecord(0);

                //Field Name = xrmApp.Entity.GetField("fax");

                //Assert.IsFalse(Name.IsRequired);

                //xrmApp.ThinkTime(3000);

                var element = driver.FindElement(By.Id("fax"));
                Assert.IsTrue(element.Displayed);
                Assert.AreEqual(element.Text.ToLower(), "Expected text".ToLower());
            }
        }

        [TestMethod]
        public void UCITestConfirmFieldIsRequired()
        {
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);

                xrmApp.Navigation.OpenApp(UCIAppName.CustomerService);

                xrmApp.Navigation.OpenSubArea("CustomerService", "Accounts");

                xrmApp.Grid.Search("test1");

                xrmApp.Grid.OpenRecord(0);

                Field Name = xrmApp.Entity.GetField("name");

                Assert.IsTrue(Name.IsRequired);

                xrmApp.ThinkTime(3000);

            }
        }


        [TestMethod]
        [TestCategory("Validate if a field has value")]
        public void UCITestConfirmFieldContainsData()
        {
            //Test to validate if a field has value or if it is null

            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);

                xrmApp.Navigation.OpenApp(UCIAppName.CustomerService);

                xrmApp.Navigation.OpenSubArea("CustomerService", "Accounts");

                xrmApp.Grid.Search("test1");

                xrmApp.Grid.OpenRecord(0);

                //Field faxField = xrmApp.Entity.GetField("fax");

                string faxValue = xrmApp.Entity.GetValue("fax");

                //var faxNumber = faxValue;

                //xrmApp.Entity.GetType();

                Assert.IsNotNull(faxValue); //check it because it should returns an error as faxNumber is null or ""
                              
                //Assert.IsFalse(true);


                xrmApp.ThinkTime(3000);

            }
        }
    }

}