using BugSharp;

namespace Example;

public class BugZillaExample
{
    private readonly BugZilla _bugZilla;
    
    public BugZillaExample()
    {
        _bugZilla = BugZilla.Create("URL_TO_BUGZILLA", "OPTIONAL_API_KEY");

        GetBug(4650);

        Console.ReadKey();
    }

    private async void GetBug(int bugId)
    {
        var bug = await _bugZilla.Bugs.GetBugAsync(bugId);
        var comments = await _bugZilla.Comments.GetCommentsAsync(bug.Id);

        Console.WriteLine(comments.Count + " comments");
        
        foreach (var comment in comments)
        {
            Console.WriteLine(comment.Text);
        }
    }
}