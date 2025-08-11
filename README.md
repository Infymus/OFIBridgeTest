**Once compiled and ready - Enable Oracle Access Bridge**
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
