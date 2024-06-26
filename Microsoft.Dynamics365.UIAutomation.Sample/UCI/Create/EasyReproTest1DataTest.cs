﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using OpenQA.Selenium;
using Microsoft.Dynamics365.UIAutomation.Browser.Logs;

namespace Microsoft.Dynamics365.UIAutomation.Sample.UCI
{
    [TestClass]
    public class EasyReproTest1DataTest
    {
        public TestContext TestContext { get; set; }

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly SecureString _mfaSecretKey = System.Configuration.ConfigurationManager.AppSettings["MfaSecretKey"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        [TestCategory("TestersTalk")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "C:\\Users\\RobysonWillyandePaul\\Desktop\\Projects\\EasyReproTestAutomation\\Microsoft.Dynamics365.UIAutomation.Sample\\UCI\\Create\\testdata.xml",
            "testdata", DataAccessMethod.Sequential)]
        public void UCITestCreateAccount()
        {
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);

                xrmApp.Navigation.OpenApp(UCIAppName.CustomerService);

                xrmApp.Navigation.OpenSubArea("CustomerService", "Accounts");

                xrmApp.CommandBar.ClickCommand("New");

                xrmApp.Entity.SetValue("name", TestContext.DataRow["tag1"].ToString());

                xrmApp.Entity.Save();

            
            }
            
        }
    }
}