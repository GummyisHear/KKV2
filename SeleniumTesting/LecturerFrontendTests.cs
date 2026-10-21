using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace KeelteKoolV2.SeleniumTesting;

public class LecturerFrontendTests
{
    private const string BaseUrl = "http://localhost:5117";

    [Fact]
    public void Should_CreateLecturer_FromFrontend()
    {
        using IWebDriver driver = new FirefoxDriver();

        driver.Navigate().GoToUrl($"{BaseUrl}/Lecturers");

        driver.FindElement(By.Id("IndexCreateNewLecturer_BTN")).Click();

        driver.FindElement(By.Id("CU_Lecturer_FirstName"))
            .SendKeys("Selenium");

        driver.FindElement(By.Id("CU_Lecturer_LastName"))
            .SendKeys("Lecturer");

        driver.FindElement(By.Id("CU_Lecturer_Qualifications"))
            .SendKeys("Test");

        driver.FindElement(By.Id("CU_Save_BTN")).Click();

        var pageSource = driver.PageSource;

        Assert.Contains("Selenium", pageSource);
        Assert.Contains("Lecturer", pageSource);
        Assert.Contains("Test", pageSource);
    }
}