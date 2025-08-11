using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Data;
using System.Reflection;
using DataBase.Query;
using TestHelpers.Enums;
using TestCases.TestMethods;
using TestHelpers.Objects;
using Tests.TestMethods;
using Tests.Utilities;
using OFIBridgeTest.Tests.NodeExtensions;

namespace OFIBridgeTest.Tests.TestCases.SampleTestCases
{

    [TestFixture]
    public class SampleTest : BaseTest
    {
        public SampleTest()
        {
            // This is the URL location to the Oracle Form Area. Each of these is different.
            InitialUrl = "/OA_HTML/RF.jsp?function_id=1052325&resp_id=21623&resp_appl_id=660";
        }

        [Category("Smoke_Tests")]
        [TestCase("SmokeTests", TestCaseParams.Param1)]
        public void RunSmokeTest(string testCase, TestCaseParams inTestCaseParam1)
        {
            SampleTestCase(testCase, inTestCaseParam1);
        }

        [Category("Order_Management")]

        [TestCase("TESTCASENUM_TESTCASENAME", TestCaseParams.Param1)]

        public void SampleTestCase(string testCase, TestCaseParams inParam1)
        {
            try // Start of Test Run
            {
                DebugOutputSep();
                DebugOutput($"inTestCaseID = {testCase}");
                DebugOutputSep();

                // Do some BaseStuff Here
                string orderID = "51";
                DataTable dt = OracleQuery.dbGetDataTable(orderID);

                // ########################################## TEST RUN FROM HERE #################################################

                // Maximize the Java JNLP File
                OracleFormsExtensions.ClickCloseForm(Window, true);
                if (!BaseTestNoLaunch)
                    OracleFormsExtensions.OracleFormsMaximizeJava(Window);


                // Run a Test Method that does somethign
                OracleFormAreas.SampleTestMethod(Window);

                // ## VALIDATION - VALIDATE ALL DATA FROM FORMS AGAINST ORACLE ##

                DebugOutput("VALIDATION - VALIDATE ALL DATA FROM FORMS AGAINST ORACLE");

                // Do your Assertions Here

                // #############################################################################
                // ## TEST COMPLETE ##
                // #############################################################################

                // Test Finished, Write out Debug Log
                DebugOutput($"**** TEST PASSED");
            }
            catch (SuccessException ex)
            {
                DebugOutput($"**** TEST PASSED: {ex.Message}");
                TakeScreenShotAttach("TestPassScreenShot");
                throw;
            }
            catch (InconclusiveException ex)
            {
                DebugOutput($"**** TEST INCONCLUSIVE: {ex.Message}");
                TakeScreenShotAttach("TestInconclusiveScreenShot");
                throw;
            }
            catch (Exception ex)
            {
                DebugOutput($"**** TEST FAILED: {ex.Message}");
                TakeScreenShotAttach("TestFailureScreenShot");
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
