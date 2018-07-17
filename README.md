External logging project
============================

>**Latest supported version**: Sitefinity CMS 11.0.6700.0

>**Documentation articles**: [Tutorial: Create and enable a custom trace listener](http://docs.sitefinity.com/tutorial-create-and-enable-a-custom-trace-listener)

### Overview

Developers can implement their own custom error logger. This sample demonstrates error logging to an external application, which in this case is [Raygun.io](https://raygun.io/).

### Prerequisites
- You must have a Sitefinity CMS license.
- Your setup must comply with the system requirements.  
 For more information, see the [System requirements](https://docs.sitefinity.com/system-requirements) for the  respective Sitefinity CMS version.
- Obtain a Raygun API key.  
 To acquire a Raygun API key register in [Raugun.io](https://raygun.io/) and create a new application on your Raygun.io dashboard.

### Installation

1. Clone the sample repository.
2. Clear the NuGet cache files.  
 a. Open the solution file in Visual Studio.  
 b. In the toolbar, navigate to _Tools >> NuGet Package Manager >> Package Manager Settings_.  
 c. In the left pane, navigate to _NuGet Package Manager >> General_.  
 d. Click _Clear All NuGet Cache(s)_.  
3. Restore the NuGet packages in the solution.  
   
   >**NOTE**: The solution in this repository relies on NuGet packages with automatic package restore while the build procedure takes place.   
   >For a full list of the referenced packages and their versions see the [packages.config](http://github.com/Sitefinity-SDK/external-logging/blob/master/ExternalLogging/packages.config) file.    
   >For a history and additional information related to package versions on different releases of this repository, see the [Releases page](http://github.com/Sitefinity-SDK/external-logging/releases).
   >  
   a. Navigate to _Tools >> NuGet Package Manager >> Package Manager Console_.  
   b. In _Source_, select Sitefinity CMS NuGet Repository.  
   c. Click _Restore_ button.
2. Open your Sitefinity CMS application in Visual Studio.
3. In the context menu of the solution, click _Add_ >> _Existing project…_
4. Browse to the folder of the cloned project and select the `ExternalLogging.csproj` file.
6. In your project, in the context menu of the _Reference_ folder, click _Add_ >> _References…_
7. Select the newly added `ExternalLogging` project.
8. Open the `web.config` file of your Sitefinity CMS application and configure Raygun:  
  - Inside the `<configSections>` tag, add the following section:  
   ```<section name="RaygunSettings" type="Mindscape.Raygun4Net.RaygunSettings, Mindscape.Raygun4Net"/>```
  - After the `<configSections>` tag, add the Raygun settings configuration block:  
   ```<RaygunSettings apikey="YOUR_APP_API_KEY" />```   
9. Build your solution.

>**RESULT**: You can monitor your application in the Dashboard of your Reygun application. For more information, see [Raugun.io](https://raygun.io/).

### Additional resources

#### Youtube video demo:

[![Tooltip](https://raw.githubusercontent.com/Sitefinity-SDK/external-logging/master/externalLogging.png)](http://youtu.be/-L_99f7UjZ8)
