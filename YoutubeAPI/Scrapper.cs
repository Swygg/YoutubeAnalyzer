using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace YoutubeAPI
{

    public class Scrapper
    {
        IWebDriver driver;
        string randomYoutubeVideo = "https://www.youtube.com/user/LesTutosdeHuito/videos";
        string google = "https://www.google.fr";

        public Scrapper()
        {
            driver = GetChromeDriver();
            driver.Url = randomYoutubeVideo;
            Thread.Sleep(2000);
            var IAgreeButton = GetIAgreeButton(driver);
            if (IAgreeButton == null)
                throw new Exception("No 'I Agree' button found");
            IAgreeButton.Click();
            driver.Url = google;
        }

        public string GetHtmlFromUrl(string url)
        {
            driver.Url = url;
            ScrollUntilTheEnd(driver);
            return driver.PageSource;
        }

        public void Close()
        {
            driver.Close();
            driver.Quit();
        }

        public void ScrollUntilTheEnd(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            Int64 heightBefore;
            Int64 heightAfter;
            while (true)
            {
                heightBefore = (Int64)js.ExecuteScript("return document.documentElement.scrollHeight");
                js.ExecuteScript("var scrollingElement = (document.scrollingElement || document.body);scrollingElement.scrollTop = scrollingElement.scrollHeight;");
                Thread.Sleep(1000);
                heightAfter = (Int64)js.ExecuteScript("return document.documentElement.scrollHeight");

                if (heightBefore == heightAfter)
                    break;
            }
        }


        private IWebDriver GetChromeDriver()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            return new ChromeDriver(chromeDriverService, new ChromeOptions());
        }

        private static IWebElement GetIAgreeButton(IWebDriver driver)
        {
            var tempo = driver.FindElements(By.ClassName("VfPpkd-LgbsSe"));
            foreach (var t in tempo)
            {
                if (t.Text == "J'ACCEPTE")
                    return t;
            }
            return null;
        }
    }
}
