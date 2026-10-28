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
        Assert.Contains("/Lecturers", driver.Url);
        Assert.DoesNotContain("/Create", driver.Url);
    }

    [Fact]
    public void Should_ShowLecturerDetails_FromFrontend()
    {
        using IWebDriver driver = new FirefoxDriver();

        driver.Navigate().GoToUrl($"{BaseUrl}/Lecturers");

        driver.FindElement(By.Id("IndexCreateNewLecturer_BTN")).Click();

        driver.FindElement(By.Id("CU_Lecturer_FirstName"))
            .SendKeys("DetailsTest");

        driver.FindElement(By.Id("CU_Lecturer_LastName"))
            .SendKeys("LecturerTest");

        driver.FindElement(By.Id("CU_Lecturer_Qualifications"))
            .SendKeys("Teacher");

        driver.FindElement(By.Id("CU_Save_BTN")).Click();

        var detailsRows = driver.FindElements(By.TagName("tr"));

        foreach (var row in detailsRows)
        {
            if (row.Text.Contains("DetailsTest"))
            {
                row.FindElement(By.LinkText("Details")).Click();
                break;
            }
        }

        var firstName = driver.FindElement(By.Id("DD_Lecturer_FirstName")).Text;
        var lastName = driver.FindElement(By.Id("DD_Lecturer_LastName")).Text;
        var qualifications = driver.FindElement(By.Id("DD_Lecturer_Qualifications")).Text;

        Assert.Equal("DetailsTest", firstName);
        Assert.Equal("LecturerTest", lastName);
        Assert.Equal("Teacher", qualifications);
        Assert.Contains("Lecturer Details", driver.PageSource);
        Assert.Contains("/Lecturers/Details", driver.Url);
    }

    [Fact]
    public void Should_UpdateLecturer_FromFrontend()
    {
        using IWebDriver driver = new FirefoxDriver();

        driver.Navigate().GoToUrl($"{BaseUrl}/Lecturers");

        driver.FindElement(By.Id("IndexCreateNewLecturer_BTN")).Click();

        driver.FindElement(By.Id("CU_Lecturer_FirstName"))
            .SendKeys("UpdateTest");

        driver.FindElement(By.Id("CU_Lecturer_LastName"))
            .SendKeys("LecturerTest");

        driver.FindElement(By.Id("CU_Lecturer_Qualifications"))
            .SendKeys("Teacher");

        driver.FindElement(By.Id("CU_Save_BTN")).Click();

        var updateRows = driver.FindElements(By.TagName("tr"));

        foreach (var row in updateRows)
        {
            if (row.Text.Contains("UpdateTest"))
            {
                row.FindElement(By.LinkText("Update")).Click();
                break;
            }
        }

        var firstNameInput = driver.FindElement(By.Id("CU_Lecturer_FirstName"));

        firstNameInput.Clear();
        firstNameInput.SendKeys("UpdatedName");

        driver.FindElement(By.Id("CU_Save_BTN")).Click();

        Assert.Contains("UpdatedName", driver.PageSource);
        Assert.Contains("LecturerTest", driver.PageSource);
        Assert.Contains("Teacher", driver.PageSource);
        Assert.Contains("/Lecturers", driver.Url);
        Assert.DoesNotContain("/Update", driver.Url);
    }

    [Fact]
    public void Should_DeleteLecturer_FromFrontend()
    {
        using IWebDriver driver = new FirefoxDriver();

        driver.Navigate().GoToUrl($"{BaseUrl}/Lecturers");

        driver.FindElement(By.Id("IndexCreateNewLecturer_BTN")).Click();

        driver.FindElement(By.Id("CU_Lecturer_FirstName"))
            .SendKeys("DeleteTest");

        driver.FindElement(By.Id("CU_Lecturer_LastName"))
            .SendKeys("LecturerTest");

        driver.FindElement(By.Id("CU_Lecturer_Qualifications"))
            .SendKeys("Teacher");

        driver.FindElement(By.Id("CU_Save_BTN")).Click();

        var deleteRows = driver.FindElements(By.TagName("tr"));

        foreach (var row in deleteRows)
        {
            if (row.Text.Contains("DeleteTest"))
            {
                row.FindElement(By.LinkText("Delete")).Click();
                break;
            }
        }

        Assert.Contains("Delete Lecturer", driver.PageSource);
        Assert.Contains("DeleteTest", driver.PageSource);
        Assert.Contains("LecturerTest", driver.PageSource);

        driver.FindElement(By.Id("DeleteDelete_BTN")).Click();

        Assert.DoesNotContain("DeleteTest", driver.PageSource);
        Assert.Contains("/Lecturers", driver.Url);
    }
}