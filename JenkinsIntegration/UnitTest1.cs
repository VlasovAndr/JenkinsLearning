using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using WebDriverManager.DriverConfigs.Impl;

namespace JenkinsIntegration
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            // var options = new ChromeOptions();

            // options.AddUserProfilePreference("download.prompt_for_download", false);
            // options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            // options.AddUserProfilePreference("browser.download.manager.showWhenStarting", false);
            // options.AddUserProfilePreference("safebrowsing.enabled", "true");
            // options.AddArgument("no-sandbox");
            // options.AddArgument("disable-popup-blocking");
            // options.AddUserProfilePreference("profile.cookie_controls_mode", 0);

            // var specificDriver = new ChromeDriver(options);

            // specificDriver.Manage().Window.Maximize();


            // SELENOID
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddUserProfilePreference("download.default_directory", directory);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            chromeOptions.AddUserProfilePreference("browser.download.manager.showWhenStarting", false);
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", "true");
            chromeOptions.AddArgument("no-sandbox");
            chromeOptions.AddArgument("disable-popup-blocking");
            chromeOptions.AddAdditionalOption("selenoid:options", new Dictionary<string, object>
            {
                ["enableLog"] = true,
                ["enableVnc"] = true,
                ["enableVideo"] = false
            });
            var specificDriver = new RemoteWebDriver(new Uri("http://selenoid:4444/wd/hub"), chromeOptions.ToCapabilities());


            specificDriver.Navigate().GoToUrl("https://demoqa.com/");
            Thread.Sleep(5);
            specificDriver.Quit();
        }
    }
}