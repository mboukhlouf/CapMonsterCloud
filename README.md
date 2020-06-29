# CapMonsterCloud
[![AppVeyor](https://img.shields.io/appveyor/build/MohammedBoukhlouf/capmonstercloud?style=flat-square)](https://ci.appveyor.com/project/MohammedBoukhlouf/capmonstercloud)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/M-Boukhlouf/CapMonsterCloud?style=flat-square)](https://github.com/M-Boukhlouf/CapMonsterCloud/releases/latest) 
[![Nuget](https://img.shields.io/nuget/v/CapMonsterCloud?style=flat-square)](https://www.nuget.org/packages/CapMonsterCloud) 
[![GitHub](https://img.shields.io/github/license/M-Boukhlouf/CapMonsterCloud?style=flat-square)](https://github.com/M-Boukhlouf/CapMonsterCloud/blob/master/LICENSE)

This is a C# wrapper for [CapMonster Cloud](https://capmonster.cloud/en/) API and provides methods to create a task and get the result of the task.

# Installation
Via Nuget:
```
Install-Package CapMonsterCloud
```

# Usage
### Creating a client
```c#
// Create a client with your client key
var clientKey = "YOUR_CLIENT_KEY";
var client = new CapMonsterClient(clientKey);
```

### Get balance
```c#
var balance = await client.GetBalanceAsync();
```

### Create a task
Task types:
- FunCaptchaTaskProxyless
- ImageToTextTask
- NoCaptchaTask
- NoCaptchaTaskProxyless
- RecaptchaV3TaskProxyless

```c#
// Creating a NoCaptchaTaskProxyless task object
var captchaTask = new NoCaptchaTaskProxyless
{
    WebsiteUrl = "WEBSITE_URL",
    WebsiteKey = "WEBSITE_KEY",
    UserAgent = "USER_AGENT"
};
// Create the task and get the task id
int taskId = await client.CreateTaskAsync(captchaTask);
```
### Get task result
Task result types:
- FunCaptchaTaskProxylessResult
- ImageToTextTaskResult
- NoCaptchaTaskProxylessResult
- NoCaptchaTaskResult
- RecaptchaV3TaskProxylessResult

```c#
// Get the task result
var solution = await client.GetTaskResultAsync<NoCaptchaTaskProxylessResult>(taskId);
// Recaptcha response to be used in the form
var recaptchaResponse = solution.GRecaptchaResponse;
```	

# Error handling
```c#
try 
{
    int taskId = await client.CreateTaskAsync(captchaTask);
}
catch (CapMonsterException e)
{
    Console.WriteLine($"{e.ErrorCode}: {e.ErrorDescription}");
}
```
