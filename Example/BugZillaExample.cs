using BugSharp;

namespace Example;

public class BugZillaExample
{
    private readonly BugZilla _bugZilla;
    
    public BugZillaExample()
    {
        _bugZilla = BugZilla.Builder()
            .SetUrl("https://portal.consoden.se/bugslb/")
            .SetApiKey("sPe3VLMMtGCR8IpKqp7LhaV0Kvj36RemgHvUrkWu")
            .Build();

        GetBug(4650);
    }

    private async void GetBug(int bugId)
    {
        var bug = await _bugZilla.Bugs.GetBugAsync(bugId);
    }
}