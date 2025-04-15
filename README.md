# Jonteohr.BugSharp
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

#### Fetch a bug:
```csharp
var bug = await client.Bugs.GetBugAsync(123); // 123 being a single bug ID
Console.WriteLine($"Bug summary: {bug.Summary}");
```

#### Get several bugs:
```csharp
var bugs = await _client.Bugs.GetBugsAsync([123, 321, 546]); // An array of bug IDs
bugs.ForEach(b => Console.WriteLine($"Bug #{b.Id}: {b.Summary}"));
```

#### Search for bugs:
```csharp
// Create the BugZilla search object
var search = _client.CreateSearch();

// Fill it with data
search.Product = "MyProduct";
search.QuickSearch = "status:unco";

// Store the results of the search
var results = await search.SearchBugsAsync();
Console.WriteLine($"Found {results.Count} bugs matching the search criteria!");
```

## Supports
List of implemented API calls from [Bugzilla Rest API](https://bugzilla.readthedocs.io/en/5.2/api/core/v1/index.html)
- ✅ Attachments
- ✅ Bugs
  - ✅ Bug Search
- ❌ Bugzilla Information
- ❌ Classifications
- ✅ Comments
- ✅ Components
- ✅ Bug Fields
- ❌ Flag Types
- ❌ Groups
- ❌ Products
- ❌ Users