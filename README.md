External logging
============================
As of Sitefinity 7.1, developers can implement their own custom error logger. This sample demonstrates error logging to an external application, which in this case is [Raygun.io](https://raygun.io/).

### Requirements

* Sitefinity license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions

* Windows Identity Foundation

   NOTE: Depending on the Microsoft OS version you are using, the method for downloading and installing or enabling the identity framework differs:

  * Windows 7 - download from [Windows Identity Foundation](http://www.microsoft.com/en-us/download/details.aspx?id=17331)

  * Windows 8 - in the Control Panel, turn on the relevant Windows feature Windows Identity Foundation 3.5* Windows Identity Foundation

### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.

You need a Raygun API key, in order to run this sample. To acquire a Raygun API key register in [Raugun.io](https://raygun.io/) and create a new application on your Raygun.io dashboard.


### Installation instructions

1. Clone the sample repository
2. Open your Sitefinity application in Visual Studio.
3. From the context menu of the solution, select *Add* » *Existing project…*
4. Browse to the folder of the custom error trace listener
5. Select the ExternalLogging.csproj file
6. From the context menu of the *Reference* folder in your project, select *Add* » *References…*
7. Select the newly added **ExternalLogging** project
8. Open the **web.config** file of your Sitefinity application and configure Raygun:
  * Add the following section to configSections: 
   ```<section name="RaygunSettings" type="Mindscape.Raygun4Net.RaygunSettings, Mindscape.Raygun4Net"/>```
  * Add the Raygun settings configuration block: 
   ```<RaygunSettings apikey="YOUR_APP_API_KEY" />```
  * **NOTE**: You can check the [Raygun GitHub readme](https://github.com/MindscapeHQ/raygun4net/blob/master/README.md) for additional information on how to configure your application's **web.config** file.
9. Build your solution

### Additional resources

Youtube video demo:

[![Tooltip](https://raw.githubusercontent.com/Sitefinity-SDK/external-logging/develop/externalLogging.png)](http://youtu.be/YXXjEGRE_PE)

[Trace listener tutorial](http://www.sitefinity.com/documentation/documentationarticles/tutorials-personalization)
