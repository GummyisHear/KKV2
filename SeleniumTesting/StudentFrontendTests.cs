using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace KeelteKoolV2.SeleniumTesting;

[TestCaseOrderer("KeelteKoolV2.SeleniumTesting.PriorityOrderer", "KeelteKoolV2.SeleniumTesting")]
public class StudentFrontendTests
{
    //unikaalne Guid mida hoitakse perekonnanime väljas, selle järgi leitakse test-objekt üles
    public static string thisRunGuid { get; set; }

    private const string BaseUrl = "https://localhost:7191/Students";

    /// <summary>
    /// Test mis loob uue õpilase unikaalse perekonnanimega ning kontrollib, et õpilane ilmub Index tabelisse õigete andmetega.
    /// </summary>
    [Fact]
    [Order(1)]
    public void Should_NavigateToIndex_AddStudentWithUniqueData_ReturnToIndex()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            // tegevus 
            driver.FindElement(By.Id("IndexCreateNewStudent_BTN")).Click();
            WaitForElement(driver, "CU_Student_FirstName");
            thisRunGuid = DataEntry_Student(driver);
            driver.FindElement(By.Id("CU_Create_BTN")).Click();

            // kontroll 
            WaitForElement(driver, "IndexTable");
            IWebElement row = FindStudentRow(driver, thisRunGuid);
            Assert.NotNull(row);
            Assert.Equal("Nikita", row.FindElement(By.Id("IndexTableStudentFirstName")).Text);
            Assert.Equal(thisRunGuid, row.FindElement(By.Id("IndexTableStudentLastName")).Text);
            Assert.Equal("MA, X, 2", row.FindElement(By.Id("IndexTableStudentGrades")).Text);
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis avab varem lisatud õpilase andmete lehe ning kontrollib, et kõik väljad on täidetud ja vastavad sisestatud andmetele.
    /// </summary>
    [Fact]
    [Order(2)]
    public void Should_NavigateToDetailsOfAStudent_WithPreviouslyUniqueData()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            IWebElement row = FindStudentRow(driver, thisRunGuid);
            bool isObjectFound = row != null;

            // tegevus 
            row.FindElement(By.Id("IndexTableStudentActions"))
                .FindElement(By.Id("IndexTableStudentDetails_BTN")).Click();
            WaitForElement(driver, "DD_StudentGuid");

            string details_id = driver.FindElement(By.Id("DD_StudentGuid")).Text;
            string details_firstName = driver.FindElement(By.Id("DD_StudentFirstName")).Text;
            string details_lastName = driver.FindElement(By.Id("DD_StudentLastName")).Text;
            string details_grades = driver.FindElement(By.Id("DD_StudentGrades")).Text;
            string details_createdAt = driver.FindElement(By.Id("DD_StudentCreatedAt")).Text;

            // kontroll 
            Assert.True(isObjectFound);
            Assert.Equal("Õpilase andmed", driver.FindElement(By.TagName("h1")).Text);
            Assert.NotEmpty(details_id);
            Assert.True(Guid.TryParse(details_id, out _));
            Assert.NotEmpty(details_createdAt);
            Assert.Equal("Nikita", details_firstName);
            Assert.Equal(thisRunGuid, details_lastName);
            Assert.Equal("MA, X, 2", details_grades);
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis vajutab andmete lehel "Tagasi" nuppu ja kontrollib, et kasutaja jõuab tagasi Index lehele
    /// ning õpilane on tabelis endiselt olemas.
    /// </summary>
    [Fact]
    [Order(3)]
    public void Should_ReturnToIndex_WhenBackIsClickedInDetailsView()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            ClickRowAction(driver, thisRunGuid, "IndexTableStudentDetails_BTN");
            WaitForElement(driver, "DetailsBack_BTN");

            // tegevus 
            driver.FindElement(By.Id("DetailsBack_BTN")).Click();

            // kontroll 
            WaitForElement(driver, "IndexTable");
            Assert.NotNull(FindStudentRow(driver, thisRunGuid));
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis vajutab loomislehel "Tagasi" nuppu ja kontrollib, et uut õpilast ei lisatud (ridade arv on sama).
    /// </summary>
    [Fact]
    [Order(4)]
    public void Should_NotAddStudent_WhenCancelIsClickedInCreateView()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            int rowCountBefore = driver.FindElements(By.CssSelector("#IndexTable tbody tr")).Count;

            // tegevus 
            driver.FindElement(By.Id("IndexCreateNewStudent_BTN")).Click();
            WaitForElement(driver, "CU_Student_FirstName");
            string unsavedGuid = DataEntry_Student(driver); //andmed sisestatakse, aga EI salvestata
            driver.FindElement(By.Id("CU_Cancel_BTN")).Click();

            // kontroll 
            WaitForElement(driver, "IndexTable");
            int rowCountAfter = driver.FindElements(By.CssSelector("#IndexTable tbody tr")).Count;
            Assert.Equal(rowCountBefore, rowCountAfter);
            Assert.Null(FindStudentRow(driver, unsavedGuid));
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis kontrollib, et loomisleht avaneb õige pealkirja ja tühjade väljadega ning
    /// et "Loo" nupp on olemas, aga "Muuda" nuppu ei ole.
    /// </summary>
    [Fact]
    [Order(5)]
    public void Should_OpenCreateViewWithEmptyFields_WhenCreateButtonIsClicked()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            // tegevus 
            driver.FindElement(By.Id("IndexCreateNewStudent_BTN")).Click();
            WaitForElement(driver, "CU_Student_FirstName");

            // kontroll 
            Assert.Equal("Lisa uus õpilane", driver.FindElement(By.TagName("h1")).Text);
            Assert.Equal("", driver.FindElement(By.Id("CU_Student_FirstName")).GetAttribute("value"));
            Assert.Equal("", driver.FindElement(By.Id("CU_Student_LastName")).GetAttribute("value"));
            Assert.Equal("", driver.FindElement(By.Id("CU_Student_Grades")).GetAttribute("value"));
            Assert.Single(driver.FindElements(By.Id("CU_Create_BTN")));
            Assert.Empty(driver.FindElements(By.Id("CU_Update_BTN")));
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis vajutab andmete lehel "Muuda" nuppu ja kontrollib, et muutmisleht avaneb
    /// õpilase olemasolevate andmetega eeltäidetuna.
    /// </summary>
    [Fact]
    [Order(6)]
    public void Should_OpenUpdateViewWithExistingData_WhenUpdateIsClickedInDetailsView()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            ClickRowAction(driver, thisRunGuid, "IndexTableStudentDetails_BTN");
            WaitForElement(driver, "DetailsUpdate_BTN");

            // tegevus 
            driver.FindElement(By.Id("DetailsUpdate_BTN")).Click();
            WaitForElement(driver, "CU_Student_FirstName");

            // kontroll 
            Assert.Equal("Muuda õpilast", driver.FindElement(By.TagName("h1")).Text);
            Assert.Equal("Nikita", driver.FindElement(By.Id("CU_Student_FirstName")).GetAttribute("value"));
            Assert.Equal(thisRunGuid, driver.FindElement(By.Id("CU_Student_LastName")).GetAttribute("value"));
            Assert.Equal("MA, X, 2", driver.FindElement(By.Id("CU_Student_Grades")).GetAttribute("value"));
            Assert.Single(driver.FindElements(By.Id("CU_Update_BTN")));
            Assert.Empty(driver.FindElements(By.Id("CU_Create_BTN")));
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis muudab varem lisatud õpilase eesnime ja hindeid (perekonnanimi jääb samaks) ning
    /// kontrollib andmete lehel, et uued andmed on salvestunud ja vanad asendatud.
    /// </summary>
    [Fact]
    [Order(7)]
    public void Should_UpdateDetailsOfUniqueStudent_WithNewData()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            bool isObjectFound = FindStudentRow(driver, thisRunGuid) != null;

            // tegevus 
            ClickRowAction(driver, thisRunGuid, "IndexTableStudentUpdate_BTN");
            WaitForElement(driver, "CU_Student_FirstName");
            DataClear_CreateUpdate_SaveLastName(driver);
            DataEntry_Student(driver, true, false);
            driver.FindElement(By.Id("CU_Update_BTN")).Click();

            // kontroll 
            //navigeerime uuesti Indexisse, et olla kindel et andmed tuleb serverist
            driver.Url = BaseUrl;
            WaitForElement(driver, "IndexTable");
            IWebElement updatedRow = FindStudentRow(driver, thisRunGuid);
            Assert.NotNull(updatedRow);

            //kontrollime kõigepealt Index tabeli andmeid
            Assert.Equal("Uusnimi", updatedRow.FindElement(By.Id("IndexTableStudentFirstName")).Text);
            Assert.Equal("5, 4", updatedRow.FindElement(By.Id("IndexTableStudentGrades")).Text);

            //ning seejärel andmete lehe andmeid
            updatedRow.FindElement(By.Id("IndexTableStudentDetails_BTN")).Click();
            WaitForElement(driver, "DD_StudentGuid");

            string details_id = driver.FindElement(By.Id("DD_StudentGuid")).Text;
            string details_firstName = driver.FindElement(By.Id("DD_StudentFirstName")).Text;
            string details_lastName = driver.FindElement(By.Id("DD_StudentLastName")).Text;
            string details_grades = driver.FindElement(By.Id("DD_StudentGrades")).Text;

            Assert.True(isObjectFound);
            Assert.NotEmpty(details_id);
            Assert.Equal(thisRunGuid, details_lastName);
            Assert.Equal("Uusnimi", details_firstName);
            Assert.NotEqual("Nikita", details_firstName);
            Assert.Equal("5, 4", details_grades);
            Assert.NotEqual("MA, X, 2", details_grades);
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis avab kustutamislehe ja kontrollib, et lehel kuvatakse just see õpilane, keda kustutada soovitakse.
    /// </summary>
    [Fact]
    [Order(8)]
    public void Should_ShowCorrectStudent_InDeleteView()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            // tegevus 
            ClickRowAction(driver, thisRunGuid, "IndexTableStudentDelete_BTN");
            WaitForElement(driver, "DeleteDelete_BTN");

            // kontroll 
            Assert.Equal("Kustuta õpilane", driver.FindElement(By.TagName("h1")).Text);
            Assert.Equal("Uusnimi", driver.FindElement(By.Id("DD_StudentFirstName")).Text);
            Assert.Equal(thisRunGuid, driver.FindElement(By.Id("DD_StudentLastName")).Text);
            Assert.Equal("5, 4", driver.FindElement(By.Id("DD_StudentGrades")).Text);
            Assert.Single(driver.FindElements(By.Id("DeleteDelete_BTN")));
            Assert.Single(driver.FindElements(By.Id("DeleteCancel_BTN")));
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis vajutab kustutamislehel "Tühista" nuppu ja kontrollib, et õpilane jääb tabelisse alles.
    /// </summary>
    [Fact]
    [Order(9)]
    public void Should_KeepStudent_WhenCancelIsClickedInDeleteView()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            ClickRowAction(driver, thisRunGuid, "IndexTableStudentDelete_BTN");
            WaitForElement(driver, "DeleteCancel_BTN");

            // tegevus 
            driver.FindElement(By.Id("DeleteCancel_BTN")).Click();

            // kontroll 
            WaitForElement(driver, "IndexTable");
            Assert.NotNull(FindStudentRow(driver, thisRunGuid));
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Test mis kustutab varem lisatud õpilase kustutamislehelt ja kontrollib, et õpilast pole enam Index tabelis.
    /// </summary>
    [Fact]
    [Order(10)]
    public void Should_DeleteUniqueStudent_FromDeleteView()
    {
        // ülesseade 
        IWebDriver driver = TestSetup_NavigateToIndex();
        try
        {
            bool isObjectFoundBefore = FindStudentRow(driver, thisRunGuid) != null;

            // tegevus 
            ClickRowAction(driver, thisRunGuid, "IndexTableStudentDelete_BTN");
            WaitForElement(driver, "DeleteDelete_BTN");
            driver.FindElement(By.Id("DeleteDelete_BTN")).Click();

            // kontroll 
            WaitForElement(driver, "IndexTable");
            bool isObjectFoundAfter = FindStudentRow(driver, thisRunGuid) != null;

            Assert.True(isObjectFoundBefore);
            Assert.False(isObjectFoundAfter);
        }
        finally
        {
            driver.Quit();
        }
    }

    //eof

    /// <summary>
    /// Otsib Index tabelist rea, mille perekonnanime veerus (id IndexTableStudentLastName) on antud väärtus.
    /// </summary>
    /// <param name="driver">Hetkel käsitletav draiver</param>
    /// <param name="lastName">Otsitav perekonnanimi (unikaalne Guid)</param>
    /// <returns>Leitud tabeli rida või null kui rida ei leitud</returns>
    private static IWebElement FindStudentRow(IWebDriver driver, string lastName)
    {
        var rows = driver.FindElements(By.CssSelector("#IndexTable tbody tr"));
        foreach (var row in rows)
        {
            if (row.FindElement(By.Id("IndexTableStudentLastName")).Text == lastName)
            {
                return row;
            }
        }
        return null;
    }

    /// <summary>
    /// Leiab õpilase rea ning vajutab selles real antud id-ga tegevusnuppu (Andmed / Muuda / Kustuta).
    /// </summary>
    /// <param name="driver">Hetkel käsitletav draiver</param>
    /// <param name="lastName">Otsitav perekonnanimi (unikaalne Guid)</param>
    /// <param name="buttonId">Tegevusnupu id, nt IndexTableStudentUpdate_BTN</param>
    private static void ClickRowAction(IWebDriver driver, string lastName, string buttonId)
    {
        IWebElement row = FindStudentRow(driver, lastName);
        Assert.NotNull(row); //kui rida ei leita, ebaõnnestub test selge põhjusega
        row.FindElement(By.Id("IndexTableStudentActions"))
            .FindElement(By.Id(buttonId)).Click();
    }

    /// <summary>
    /// Ootab (kuni 10 sekundit) kuni antud id-ga element on lehel olemas.
    /// </summary>
    private static void WaitForElement(IWebDriver driver, string id)
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(By.Id(id)).Count > 0);
    }

    /// <summary>
    /// Puhastab andmeväljad välja arvatud perekonnanime väli (selles hoitakse testimiseks vajalikku unikaalset Guidi).
    /// </summary>
    /// <param name="driver">Hetkel käsitletav draiver</param>
    private static void DataClear_CreateUpdate_SaveLastName(IWebDriver driver)
    {
        driver.FindElement(By.Id("CU_Student_FirstName")).Clear();
        driver.FindElement(By.Id("CU_Student_Grades")).Clear();
    }

    /// <summary>
    /// Sisestab lehel õpilase andmed, on võimalik konfigureerida.
    /// </summary>
    /// <param name="driver">WebDriver mida hetkel test rakendab</param>
    /// <param name="isOneOrTwo">Valib kahe erineva andmekomplekti vahel (false = esimene, true = teine)</param>
    /// <param name="updateName">Kas perekonnanime väljale sisestatakse uus unikaalne Guid</param>
    /// <returns>Perekonnanime väljale sisestatud Guid stringina</returns>
    private static string DataEntry_Student(IWebDriver driver, bool isOneOrTwo = false, bool updateName = true)
    {
        Guid generatedId = Guid.NewGuid();
        if (updateName)
        {
            driver.FindElement(By.Id("CU_Student_LastName")).SendKeys(generatedId.ToString());
        }

        if (!isOneOrTwo)
        {
            driver.FindElement(By.Id("CU_Student_FirstName")).SendKeys("Nikita");
            driver.FindElement(By.Id("CU_Student_Grades")).SendKeys("MA, X, 2");
        }
        else
        {
            driver.FindElement(By.Id("CU_Student_FirstName")).SendKeys("Uusnimi");
            driver.FindElement(By.Id("CU_Student_Grades")).SendKeys("5, 4");
        }
        return generatedId.ToString();
    }

    /// <summary>
    /// Ülesseade meetod mis tekitab draiveri ja navigeerib õpilaste Index lehele.
    /// </summary>
    /// <returns>Tagastab testisiseselt kasutatava draiveri</returns>
    private static IWebDriver TestSetup_NavigateToIndex()
    {
        IWebDriver driver = new FirefoxDriver();
        driver.Url = BaseUrl;
        WaitForElement(driver, "IndexTable");
        return driver;
    }
}