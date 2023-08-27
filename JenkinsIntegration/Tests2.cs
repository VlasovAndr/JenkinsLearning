using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace JenkinsIntegration
{
    [Parallelizable(scope: ParallelScope.All)]
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("AllureSuite for Tests2 class")]
    public class Tests2
    {
        [SetUp]
        public void Setup()
        {

        }

        [AllureStep("Step for Allure2 method with params #{0} and #{1}")]
        private void Allure2(string param1, string param2)
        {
            Console.WriteLine($"Allure inside method, param1:{param1}, param2:{param2}");
        }

        [Test(Description = "Test2 Description"), Category("Test2Category")]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
        [AllureFeature("AllureFeature2")]
        [AllureStep("AllureStep Test2")]

        public void Test2()
        {
            //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //var options = new ChromeOptions();

            //options.AddUserProfilePreference("download.prompt_for_download", false);
            //options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            //options.AddUserProfilePreference("browser.download.manager.showWhenStarting", false);
            //options.AddUserProfilePreference("safebrowsing.enabled", "true");
            //options.AddArgument("no-sandbox");
            //options.AddArgument("disable-popup-blocking");
            //options.AddUserProfilePreference("profile.cookie_controls_mode", 0);

            //var specificDriver = new ChromeDriver(options);

            //specificDriver.Manage().Window.Maximize();
            //specificDriver.Navigate().GoToUrl("https://demoqa.com/");
            //Thread.Sleep(10);
            //specificDriver.Quit();

            Allure2("firstParam", "secondParam");

            //######################## REMOTE #############################
            RemoteWebDriver specificDriver;

            string browser = Environment.GetEnvironmentVariable("BROWSER_NAME");
            var selenoidUri = "http://selenoid:4444/wd/hub";


            //Set the default browser to Chrome if no value is provided
            if (string.IsNullOrEmpty(browser))
            {
                browser = "Firefox";
                selenoidUri = "http://localhost:4444/wd/hub";
            }


            //Now, you can use the 'browser' variable to launch the desired browser in your test code
            if (browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
            {
                //SELENOID
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
                specificDriver = new RemoteWebDriver(new Uri(selenoidUri), chromeOptions.ToCapabilities());
            }
            else if (browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase))
            {
                var options = new FirefoxOptions();

                options.SetPreference("browser.download.prompt_for_download", false);
                options.SetPreference("pdfjs.disabled", true);  // to always open PDF externally
                options.SetPreference("browser.download.manager.showWhenStarting", false);
                options.SetPreference("browser.safebrowsing.enabled", true);
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-popup-blocking");
                options.SetPreference("network.cookie.cookieBehavior", 0);
                options.AddAdditionalOption("selenoid:options", new Dictionary<string, object>
                {
                    ["enableLog"] = true,
                    ["enableVnc"] = true,
                    ["enableVideo"] = false
                });
                specificDriver = new RemoteWebDriver(new Uri(selenoidUri), options.ToCapabilities());
            }
            else
            {
                throw new ArgumentException("Unkniwn browser name");
            }

            specificDriver.Navigate().GoToUrl("https://demoqa.com/");
            Thread.Sleep(10);
            specificDriver.Quit();
        }
    }
}