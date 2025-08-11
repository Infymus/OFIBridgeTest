**C# Oracle Access Bridge Testing Solution**

This repository contains a C# testing solution designed to assist QA Engineers in automating tests for Oracle Forms applications. It leverages the Oracle Access Bridge to interact with the forms' elements.

**License**

This solution is completely free to use under the MIT License. Feel free to fork, modify, and distribute it as needed for your testing requirements.

**Prerequisites**

To use this solution, you will need to download and utilize the Access Bridge Explorer tool. This tool, while abandoned approximately 10 years ago, provides the necessary interoperability components.

**You can find the Access Bridge Explorer here:**

https://github.com/google/access-bridge-explorer

Important: You do not need to compile or use the entire Access Bridge Explorer solution. This testing solution only requires the WindowsAccessBridgeInterop project from its source code. Specifically, you will need the contents located at:

https://github.com/google/access-bridge-explorer/tree/master/src/WindowsAccessBridgeInterop

Integrate this specific component into your C# testing project to enable communication with the Oracle Access Bridge.