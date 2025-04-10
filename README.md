# jonteohr.BugSharp
Basic C# wrapper for the BugZilla Rest API 

## Setup
Add the api to your project from nuget

**Dotnet CLI**
```
dotnet add package jonteohr.bugsharp
```
**PackagerManager**
```
NuGet\Install-Package jonteohr.bugsharp
```

## Usage
You can find the [example project here](Example).

Create the `BugZilla` client that you need to query data:
```csharp
var client = BugZilla.Create("URL_TO_BUGZILLA", "OPTIONAL_API_KEY");
```

Then to fetch a bug:
```csharp
var bug = await client.Bugs.GetBugAsync(123);
Console.WriteLine($"Bug summary: {bug.Summary}");
```

## Supports
List of implemented API calls from [Bugzilla Rest API](https://bugzilla.readthedocs.io/en/5.2/api/core/v1/index.html)
- ✅ Attachments
- ✅ Bugs
- ❌ Bugzilla Information
- ❌ Classifications
- ✅ Comments
- ❌ Components
- ❌ Bug Fields
- ❌ Flag Types
- ❌ Groups
- ❌ Products
- ❌ Users