**C# Oracle Access Bridge Testing Solution**

This repository contains A C# testing solution designed to help quality assurance engineers test Oracle forms. 

You can't test Oracle forms with playwright or selenium because they can't access the Java JNLP window handles. You can however utilize the Oracle Access Bridge gaiuning access to all of the Java nodes for every form that is displayed in Oracle. By going through these nodes programmatically you can find the node you need by name or size or tag, and then you can manipulate that field or tag by clicking on XY or scrolling it or forcing text into a field or reading text from the node etc. It initially uses selenium to open a Chrome browser and log into Oracle forms from there it downloads the Java applet and runs it. Inside of each of the testing suites is a URL that will directly take you to the Oracle form you need however you don't need to rely on this you can use the menu and hat system I've built in. This is a great way to build out Oracle forms tests and to be able to run them locally and in Azure DevOps pipeline. Using this system I've created hundreds of tests covering all aspects of Oracle forms including manufacturing flow, AP, AR, GL, inventory, sales and shipping. 

**License**

This solution is completely free to use under the MIT License. Feel free to fork, modify, and distribute it as needed for your testing requirements.

**Prerequisites**

To use this solution, you will need to download and utilize the Access Bridge Explorer tool. This tool, while abandoned approximately 10 years ago, provides the necessary interoperability components.

**You can find the Access Bridge Explorer here:**

https://github.com/google/access-bridge-explorer

Important: You do not need to compile or use the entire Access Bridge Explorer solution. This testing solution only requires the WindowsAccessBridgeInterop project from its source code. Specifically, you will need the contents located at:

https://github.com/google/access-bridge-explorer/tree/master/src/WindowsAccessBridgeInterop

Integrate this specific component into your C# testing project to enable communication with the Oracle Access Bridge.

**Steps to get this running**

1. Download and install JAVA. You do not need to install the JDK unlesss you want to. Latest version of JRE works even back as far as 2022 as long as it has "jabswitch.exe" in the Java bin directory.
1. Enable the Oracle Access Bridge. You need to enable the OAB. Go to the directory where %JRE_HOME% is installed and find the "jabswitch" program, and run "jabswitch -enable". You should see a message that the OAB is activated.
1. Modify your AppSettings.json file to enter your Connection Strings and Oracle Strings. The DownloadDirectory is where Chrome is going to place the java JNLP. Make sure this is a directory you have access to. If it's on a build server (for pipelines) make sure the USER has read/write access.
1. Create a user secrets file and use as directed.
1. Under pipelines in Azure Devops - create a pipeline and point it at the source code.
1. BaseTest.cs is the starting point of the test. There is a line that lets you debug locally without closing or opening Chrome (it assumes you have Java already running). Set the BaseTestNoLaunch boolean accordingly to debug locally or when it runs in your pipeline.
1. Move to your TestCases > SampleTestCase > SampleTest.cs. Here you can create the basics of your nUnit test cases with multiple parameters.
1. The TestMethods > SampleTestMethod > SampleTestMethod.cs is where you can put calls in, or you can keep everything in your main method it's up to you.
1. Under NodeExtensions > All the calls to Oracle are in here. Just pass the Oracle Window (parent) window each time.
1. The only thing this project could probably be added is a way to force input into the Oracle forms using something like InputSimulatorPlus. If you find that pressing escape or control characters into the Oracle form isn't working, then use input simulator plus adding it to your project through a Nugent package.
