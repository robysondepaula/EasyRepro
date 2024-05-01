// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using OpenQA.Selenium;
using Microsoft.Dynamics365.UIAutomation.Browser.Logs;
using System.Runtime.InteropServices;
using static Microsoft.Dynamics365.UIAutomation.Browser.Constants;

namespace Microsoft.Dynamics365.UIAutomation.Sample.UCI
{
    [TestClass]
    public class EasyReproTest1
    {
        

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly SecureString _mfaSecretKey = System.Configuration.ConfigurationManager.AppSettings["MfaSecretKey"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void UCITestCreateAndDeleteAccount()
        {
            /*Test create an account, then search by the created account, enter the created account, 
              click on delete and confirm.*/
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);

                xrmApp.Navigation.OpenApp(UCIAppName.CustomerService);

                xrmApp.Navigation.OpenSubArea("CustomerService", "Accounts");

                xrmApp.CommandBar.ClickCommand("New");

                var accountName = "Test300";

                xrmApp.Entity.SetValue("name", accountName);

                xrmApp.Entity.Save();

                xrmApp.Navigation.OpenSubArea("CustomerService", "Accounts");

                xrmApp.Grid.Search("Test300");                

                xrmApp.Grid.OpenRecord(0);

                xrmApp.CommandBar.ClickCommand("Delete");

                /*To click in a button and this button has the id changing all the time in the Dynamics, 
                 we can use starts-with or contains using the by.XPath to click the dialog button*/

                client.Browser.Driver.FindElement(By.XPath("//button[starts-with(@id, 'confirmButton')]")).Click();

                //client.Browser.Driver.FindElement(By.XPath("//button[contains(@id, 'confirmButton')]")).Click();

                xrmApp.ThinkTime(4000);
            }
            
        }

        [TestMethod]
        public void UCITestSelectTabAndLookupOption()
        {
            /*Test enter into an account go to details tab back to summary tab and then select the lookup option of the 
             "parentaccountid" field, select the second option and save the account * .*/
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);

                xrmApp.Navigation.OpenApp(UCIAppName.CustomerService);

                xrmApp.Navigation.OpenSubArea("CustomerService", "Accounts");             

                xrmApp.Grid.Search("TestDetails");

                xrmApp.Grid.OpenRecord(0);
                
                xrmApp.Entity.SelectTab("Details");

                xrmApp.Entity.SelectTab("Summary");

                //Need to check if lookp is nul if is null click in "x" if not just add an parentaccount
                var paccount = xrmApp.Entity.GetField("parentaccountid");

                var test = paccount.GetType();
               
                var parentAccount = xrmApp.Entity.GetValue("parentaccountid");
              

                if (parentAccount != null /*|| parentAccount != ""*/)
                {
                    client.Browser.Driver.FindElement(By.XPath("//span[@data-id= 'parentaccountid.fieldControl-LookupResultsDropdown_parentaccountid_microsoftIcon_cancelButton']")).Click();

                    xrmApp.Entity.SetValue("parentaccountid", "Test");

                    xrmApp.Lookup.OpenRecord(0);

                    xrmApp.Entity.Save();

                    xrmApp.ThinkTime(4000);

                }
                else
                {
                    xrmApp.Entity.SetValue("parentaccountid", "Test");

                    xrmApp.Lookup.OpenRecord(0);

                    xrmApp.Entity.Save();

                    xrmApp.ThinkTime(4000);
                }
             
            }

        }
    }
}