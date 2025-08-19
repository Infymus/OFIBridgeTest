**C# Oracle Access Bridge Testing Solution**

This repository contains a C# testing solution designed to help quality assurance engineers test Oracle Forms. This solution provides a robust way to build and run automated tests for Oracle Forms both locally and in an Azure DevOps pipeline.

**Overview**

Traditional automation tools like Playwright and Selenium are unable to interact with Java JNLP windows. This solution overcomes that limitation by leveraging the **Oracle Access Bridge (OAB)**, which provides access to all Java nodes within a displayed Oracle form.

By programmatically navigating these nodes, you can:

* Find specific elements by name, size, or tag.
* Manipulate fields by clicking on coordinates, scrolling, entering text, or reading text from a node.

The testing process begins with Selenium to open a Chrome browser and log into Oracle Forms. From there, it downloads and runs the Java applet. The solution includes a menu and navigation system, but you can also directly access a specific form via a URL provided within the testing suites.

Using this framework, you can create comprehensive test suites for various aspects of Oracle Forms, including:
* Manufacturing flow
* Accounts Payable (AP)
* Accounts Receivable (AR)
* General Ledger (GL)
* Inventory
* Sales
* Shipping

**License**

This solution is freely available under the MIT License. Feel free to fork, modify, and distribute it for your testing requirements.

Prerequisites
To use this solution, you will need the **Access Bridge Explorer** tool. Although the tool itself is no longer maintained, its interoperability components are essential for this solution.

You can find the Access Bridge Explorer repository here:
<br>https://github.com/google/access-bridge-explorer

**Important:** You do not need the entire Access Bridge Explorer solution. This testing solution only requires the WindowsAccessBridgeInterop project from its source code. Specifically, you will need the contents located at:
<br>https://github.com/google/access-bridge-explorer/tree/master/src/WindowsAccessBridgeInterop

Integrate this specific component into your C# testing project to enable communication with the Oracle Access Bridge.

**Getting Started**

Follow these steps to get the testing solution up and running.

* **Install Java**: Download and install the Java Runtime Environment (JRE). You do not need the JDK. Any version of JRE that includes jabswitch.exe in its bin directory will work.

* **Enable the Oracle Access Bridge:**
  - Navigate to your Java installation directory (%JRE_HOME%).
  - Find the jabswitch program in the bin folder.
  - Open a command prompt and run jabswitch -enable. You should see a confirmation message that the OAB is activated.

* **Configure Connection Strings**: Modify the `AppSettings.json` file to enter your connection strings and Oracle-specific settings. The `DownloadDirectory` is where Chrome will place the Java JNLP file. Ensure that the user account running the tests (both locally and on a build server) has read and write access to this directory.

* **Create User Secrets**: Create a user secrets file and configure it as needed for sensitive information.

* **Set up Azure DevOps Pipeline**: This is not required to run locally but for running inside Pipelines. In Azure DevOps, create a new pipeline and point it to the source code repository. Configure your Yaml the way you like.

* **BaseTest.cs**: This is the starting point for your tests. To debug locally without opening and closing a new Chrome instance for each test, set the BaseTestNoLaunch boolean to true. When running in the pipeline, this value should be false.

* **TestCases**: Navigate to TestCases > SampleTestCase > SampleTest.cs to create the structure of your NUnit test cases, including any necessary parameters.

* **TestMethods**: Use TestMethods > SampleTestMethod > SampleTestMethod.cs to organize your method calls, or you can keep everything within your main test method.
 
* **NodeExtensions**: All the calls to Oracle Forms are located in NodeExtensions. Pass the Oracle parent window as an argument to these calls.

* **Optional**: Force Input: If you find that pressing special keys like Escape or Control within Oracle Forms isn't working, consider adding the InputSimulatorPlus NuGet package to your project. This can force input into the Oracle Forms when other methods of .SendKeys() don't work.
