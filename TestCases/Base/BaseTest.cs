using DataBase.Query;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OFIBridgeTest.Tests.NodeExtensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using Tests.Utilities;
using WindowsAccessBridgeInterop;
using Application = System.Windows.Forms.Application;

namespace TestCases.TestMethods
{
    [TestFixture]
    public class BaseTest
    {
        // ##############################################################
        // ##############################################################
        // WARNING!!! Set this to FALSE when you check this in. 
        // If you set it to TRUE, then it won't open JAVA or ORACLE.
        //
        // Set it this way:
        //
        // IF IN DEBUG MODE FOR LOCAL - SET TO: TRUE
        // IF CHECK IN FOR PIPELINE - SET TO: FALSE
        public static bool BaseTestNoLaunch = true;
        // ##############################################################
        // ##############################################################

        // BaseTest Variables
        private Process? _javaProcess;
        private Process? _chromeProcess;
        private IWebDriver? Driver;
        private WebDriverWait? Wait;
        private static WindowsAccessBridgeInterop.AccessBridge? _accessBridge;
        protected AccessibleWindow? Window;
        protected string? InitialUrl;
        string DownloadDirectory;
        public static string? OracleBaseurl;
        public static string? OracleUserName;
        public static string? OraclePassword;
        public static string? connectionString;
        public static string? SalesPersonName;
        public static OracleQuery? OracleQuery;
        private static string debugFilePath = "";

        // ######### Setup and TearDown #####################################################################

        /// <summary>
        /// Sets up Each test to run by opening Chrome and Oracle Forms, then navigating correctly
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        /// 
        [SetUp]
        public void SetupEachTest()
        {
            // Setup Debug FileName : Must be the FIRST thing in the test setup.
            string debugFileName = TestContext.CurrentContext.Test.Name;
            debugFileName = Regex.Replace(debugFileName, @"[<>:""/\\|?*'() ,]", "") + ".txt";
            debugFilePath = Path.Combine(Directory.GetCurrentDirectory(), debugFileName);

            // SetupEachTest()
            DebugOutput("SetupEachTest()");

            // Setup Oracle
            OracleUserName = "";
            OraclePassword = "";

            DebugOutput("ConfigurationBuilder()");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<BaseTest>()
                .Build();

            // Oracle Username & Connection Name
            OracleUserName = configuration["OracleUsername"];
            OraclePassword = configuration["OraclePassword"];
            var OracleConnectionName = configuration["OracleConnectionName"];

            // Oracle Java Download Directory
            DownloadDirectory = configuration["DownloadDirectory"];

            // URL Header
            OracleBaseurl = new(configuration[$"OracleUrl_{OracleConnectionName}"]);

            // DebugOutput Details for Debugging
            DebugOutput($"OracleUserName = {OracleUserName}");
            DebugOutput($"OracleConnectionName = {OracleConnectionName}");
            DebugOutput($"OracleBaseurl = {OracleBaseurl}");
            DebugOutput($"InitialUrl = {InitialUrl}");

            // Ensure the initialUrl is set
            if (!BaseTestNoLaunch)
            {
                if (string.IsNullOrEmpty(InitialUrl))
                    throw new InvalidOperationException("initialUrl must be set before running the test");
            }

            // Get Oracle Connection String and Start Database
            connectionString = configuration.GetConnectionString($"OracleConnectionString_{OracleConnectionName}");

            // Create the Oracle Query
            OracleQuery = new OracleQuery(connectionString);

            // Don't run this if we are debugging
            if (!BaseTestNoLaunch)
            {
                // Cleanup any existing Jnlp files first (not any that are open)
                DebugOutput($"Clean up existing Java JNLP files");
                DeleteJnlpFiles();

                // Close any existing Chrome or Oracle Windows
                RunCleanupScript("closeJava.bat");
                File.Delete(debugFilePath);

                // Chrome  Options
                DebugOutput("Chrome Options");
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArguments("--no-sandbox");
                options.AddArguments("--disable-gpu");
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("download.directory_upgrade", true);
                options.AddUserProfilePreference("download.default_directory", DownloadDirectory);
                options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                options.AddArgument("--allow-running-insecure-content");
                options.AddArgument("--disable-features=InsecureDownloadWarnings");
                options.AddUserProfilePreference("safebrowsing.enabled", true);

                DebugOutput("Start Chrome Driver");
                Driver = new ChromeDriver(options);
                Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

                // Get the ChromeDriver process
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                _chromeProcess = Process.GetProcessById(chromeDriverService.ProcessId);

                Login(OracleBaseurl + InitialUrl, OracleUserName, OraclePassword);
                Thread.Sleep(8000);
            }

            // Initialize Java Access Bridge
            DebugOutput("Initialize Java Access Bridge");
            _accessBridge = new WindowsAccessBridgeInterop.AccessBridge();
            _accessBridge.Initialize();
            DebugOutput("Java Access Bridge Initialized");
            Application.DoEvents();
            List<AccessibleJvm> jvms = _accessBridge.EnumJvms();

            // Handle the Oracle JAVA Security Run Box if it appears
            int tryAttempts = 1;
            int MaxAttempts = 25;
            bool FoundRunDialog = false;
            if (!BaseTestNoLaunch)
            {
                Driver?.Quit();

                do
                {
                    // Handle the security dialog first
                    DebugOutput($"Waiting for Oracle Security Run Box : tryAttempts = {tryAttempts}/{MaxAttempts}");
                    if (HandleSecurityDialog())
                    {
                        DebugOutput("OracleSecurity dialog handled");
                        FoundRunDialog = true;
                        break;
                    }
                    OracleUtilities.Sleep(3000);
                } while (tryAttempts++ < MaxAttempts);
                if (!FoundRunDialog)
                    DebugOutput("OracleSecurity dialog NOT found (not an error)");
            }

            // Wait for Java Oracle Forms to POP, this could be up to 1-2 minutes
            tryAttempts = 1;
            MaxAttempts = 50;
            do
            {
                DebugOutput($"Waiting for Oracle Forms: tryAttempts = {tryAttempts}/{MaxAttempts}");
                jvms = _accessBridge.EnumJvms();
                Application.DoEvents();

                // Display All Available Windows
                var windows = from jvm in jvms
                              from win in jvm.Windows
                              select win;
                windows.ToList().ForEach(win => DebugOutput($"| Found: {win.GetTitle()}"));
                if (!windows.Any())
                {
                    DebugOutput($"| No Oracle Windows Found");
                }

                if (!BaseTestNoLaunch)
                {
                    // By this time Oracle should be running
                    Window = (from jvm in jvms
                              from win in jvm.Windows
                              where win.GetTitle().Contains("Oracle Applications -")
                              select win).FirstOrDefault();
                    if (Window != null)
                    {
                        DebugOutput("Oracle Applications - Java application window found");
                        return;
                    }
                }
                else
                {
                    // Running locally we just need to find either the run button OR the Oracle frame
                    Window = (from jvm in jvms
                              from win in jvm.Windows
                              where win.GetTitle().Contains("Oracle Applications -")
                                 || win.GetTitle().Contains("Oracle E-Business Suite")
                              select win).FirstOrDefault();
                    if (Window != null)
                    {
                        DebugOutput("Oracle Applications - Java application window found");
                        return;
                    }
                }

                // Didn't find the window so sleep and increment attempts
                OracleUtilities.Sleep(3000);
            } while (tryAttempts++ < MaxAttempts);

            if (!BaseTestNoLaunch)
                AttachesScreenShotToTest("JavaWindowNotFound");
            DebugOutput($"Error: Java applications window not found");
            throw new Exception("Java application window not found");
        }

        /// <summary>
        /// Tears down the tests - Close Chrome and Java
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            DebugOutput("TearDown()");
            if (!BaseTestNoLaunch)
            {
                Driver?.Quit();
                RunCleanupScript("closeJava.bat");
                RunCleanup();
                TestContext.AddTestAttachment(debugFilePath);
            }
        }

        // ######### All Other Calls #####################################################################

        /// <summary>
        /// Chrome Security Dialog for Oracle Forms
        /// </summary>
        /// <param name="maxWaitTime"></param>
        /// <param name="retryInterval"></param>
        /// <returns></returns>
        public static bool HandleSecurityDialog()
        {
            List<AccessibleJvm> jvms = _accessBridge.EnumJvms();
            Application.DoEvents();
            foreach (var runButton in jvms.SelectMany(jvm =>
                         from window in jvm.Windows
                         where IsSecurityDialog(window)
                         select NodeExtensions.FindButton(window, "Run")
                         into runButton
                         where runButton != null
                         select runButton))
            {
                // Click the "Run" button
                DebugOutput($"| Found RUN Dialog");
                runButton.ClickByNode();
                return true;
            }
            DebugOutput("$| RUN Dialog not Found");
            return false;
        }

        /// <summary>
        /// Checks to see if the Node has a security dialog Name and Role
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool IsSecurityDialog(AccessibleContextNode node)
        {
            return node.GetInfo().role == "dialog" && node.GetInfo().name == "Security Information";
        }

        /// <summary>
        /// Logs into Oracle Forms intial login before Java runs
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Login(string url, string? username, string? password)
        {
            DebugOutput("CHROME: Login to Oracle Forms");
            Driver?.Navigate().GoToUrl(url);

            Wait?.Until(drv => drv.FindElement(By.Id("usernameField")));

            var usernameField = Driver.FindElement(By.Id("usernameField"));
            usernameField.SendKeys(username);

            var passwordField = Driver.FindElement(By.Id("passwordField"));
            passwordField.SendKeys(password);

            var loginButton = Driver.FindElement(By.XPath("//button[@onclick='submitCredentials()']"));
            loginButton.Click();

            DebugOutput("CHROME: Login to Oracle Forms Successful");
            
            // Handle the JNLP file
            RunJavaApp();
        }

        /// <summary>
        /// Finds the Java *.jnlp file and executes it if found
        /// </summary>
        /// <exception cref="Exception"></exception>
        protected void RunJavaApp()
        {
            DebugOutput("Execute Process to start JAVAWS()");
            var jnlpFilePath = WaitForDownload(DownloadDirectory, "jnlp");
            if (!string.IsNullOrEmpty(jnlpFilePath))
            {
                DebugOutput($"javaws -J-Ddeployment.expiration.check.enabled=false \"{jnlpFilePath}\"");

                // To bypass Updating Oracle
                _javaProcess = Process.Start("javaws", $"-J-Ddeployment.expiration.check.enabled=false \"{jnlpFilePath}\"");

                DebugOutput("JAVAWS Started");
            }
            else
            {
                throw new Exception($"JNLP File not found in {DownloadDirectory}");
            }
        }

        /// <summary>
        /// Waits for Chrome to finish downloading and then returns the files it downloaded by date
        /// </summary>
        /// <param name="downloadPath"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private static string? WaitForDownload(string downloadPath, string fileExtension)
        {
            DebugOutput($"Wait For Download From Chrome:");
            DebugOutput($"| downloadPath = '{downloadPath}'");
            DebugOutput($"| fileExtension = '{fileExtension}'");

            int tryAttempts = 1;
            int MaxAttempts = 20;

            do
            {
                DebugOutput($"Waiting for JNLP Download : tryAttempts = {tryAttempts}/{MaxAttempts}");

                var files = Directory.GetFiles(downloadPath, $"*.{fileExtension}");

                if (files.Length > 0)
                {
                    return files.OrderByDescending(f => new FileInfo(f).CreationTime).First();
                }

                OracleUtilities.Sleep(1000);
            } while (tryAttempts++ < MaxAttempts);

            DebugOutput($"Error: Java applications download failed:");
            DebugOutput($"| Check (1) Java Navigator Role Responsibilties (2) InitialURL is correct.");
            if (!BaseTestNoLaunch)
                AttachesScreenShotToTest("JavaApplicationFailedDownload");
            return null;
        }

        /// <summary>
        /// Deletes any and all leftover JNLP files
        /// </summary>
        private void DeleteJnlpFiles()
        {
            DebugOutput("DeleteJnlpFiles()");
            try
            {
                var files = Directory.GetFiles(DownloadDirectory, "*.jnlp");
                foreach (var file in files)
                {
                    File.Delete(file);
                }
                DebugOutput("JNLP files deleted successfully.");
            }
            catch (Exception ex)
            {
                DebugOutput("Failed to delete JNLP files: " + ex.Message);
            }
        }

        /// <summary>
        /// Out cleanup process
        /// </summary>
        private void RunCleanup()
        {
            DebugOutput("RunCleanup()");
            DeleteJnlpFiles();
        }

        /// <summary>
        /// Executes a batch file
        /// </summary>
        /// <param name="scriptName"></param>
        private void RunCleanupScript(string scriptName)
        {
            DebugOutput($"RunCleanupScript({scriptName})");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {scriptName}",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                DebugOutput($"RunCleanupScript Successfully completed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to run {scriptName} script: " + ex.Message);
            }
        }

        /// <summary>
        /// Attatches a screen shot to the TestContext
        /// </summary>
        /// <param name="fileName"></param>
        private static void AttachesScreenShotToTest(string fileName)
        {
            TestContext.AddTestAttachment(TakeScreenShot(fileName));
            File.Delete(fileName); 
        }

        /// <summary>
        /// For Test Areas - Pass an Area and it will take a screen shot and attach it to the Test
        /// </summary>
        /// <param name="inArea"></param>
        public static void TakeScreenShotAttach(string inArea)
        {
            if (!BaseTestNoLaunch)
            {
                string filePath = TakeScreenShot(inArea);
                DebugOutput($"TakeScreenShotAttach FileName = {inArea}.png");
                AttachesScreenShotToTest(filePath);
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Takes a screen shot and saves it - BUT DOES NOT ATTACH. Use the TakeScreenShotAttach() inside of tests.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string TakeScreenShot(string fileName)
        {
            if (!BaseTestNoLaunch)
            {
                Rectangle bounds = Screen.PrimaryScreen.Bounds;
                var screenshot = ScreenShot.CaptureScreenshot(bounds.Top, bounds.Left, bounds.Right, bounds.Bottom);
                var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.png");
                screenshot.Save(screenshotPath, ImageFormat.Png);
                return screenshotPath;
            }
            return null;
        }

        /// <summary>
        /// Adds to the Console for easy Debugging, Logging & Test Results to Azure Devops
        /// </summary>
        /// <param name="inDebugData"></param>
        public static void DebugOutput(string inDebugData)
        {
            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("MM-dd-yyyy @ hh:mm:ss tt");
            Debug.WriteLine($"{formattedDate} : {inDebugData}");
            TestContext.WriteLine($"{formattedDate} : {inDebugData}");
            if (!BaseTestNoLaunch)
            {
                File.AppendAllText(debugFilePath, $"{formattedDate} : {inDebugData}" + Environment.NewLine);
            }
        }

        /// <summary>
        /// This just writes out a line separater to make it easier to read the debug output
        /// </summary>
        public static void DebugOutputSep()
        {
            DebugOutput($"{new string('=', 60)}");
        }

    }
}
