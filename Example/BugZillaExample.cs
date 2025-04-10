using BugSharp;

namespace Example;

public class BugZillaExample
{
    private readonly BugZilla _bugZilla;
    
    public BugZillaExample()
    {
        _bugZilla = BugZilla.Create("URL_TO_BUGZILLA", "OPTIONAL_API_KEY");

        var bugInput = Console.ReadLine();
        if(!int.TryParse(bugInput, out var bugId))
            Console.WriteLine("Not a valid number");
        
        DoBugzillaStuff(bugId);
    }

    private async void DoBugzillaStuff(int bugId)
    {
        // Fetch the bug from bugzilla
        var bug = await _bugZilla.Bugs.GetBugAsync(bugId);

        Console.WriteLine($"Bug ID: {bug.Id}");
        Console.WriteLine($"Summary: {bug.Summary}");

        // Make a change to the bug
        bug.Summary = "It's another title!";
        // Save it to the server
        await bug.SaveChangesAsync();
        
        // Get all comments on that bug
        var comments = await _bugZilla.Comments.GetCommentsAsync(bugId);
        
        // Iterate through each comment, starting on the first
        foreach (var comment in comments)
        {
            Console.WriteLine($"Comment ID: {comment.Id}");
            Console.WriteLine($"Comment: {comment.Text}");
            
            if (comment.AttachmentId != -1)
            {
                // Comment has attachment, let's get it
                var attachment = await _bugZilla.Attachments.GetAttachmentAsync(comment.AttachmentId);
                
                // Let's download it
                var download = attachment.Download(Path.GetTempPath());
                Console.WriteLine($"Downloaded to: {download}");
            }
        }
        
        // Now let's create our own component
        var component = _bugZilla.CreateComponent();
        // Fill the component with required fields
        component.Name = "New Component";
        component.Product = "TestProduct";
        component.Description = "This is a test component.";
        component.DefaultAssignee = "dkl@mozilla.com";
        
        // Save it to the server
        var componentId = await component.SaveChangesAsync(); // Can also be achieved with: _bugZilla.Components.CreateComponentAsync(component);
        if(componentId != 1) // Returns -1 if failed, otherwise it returns the id of the new component
            Console.WriteLine($"Component ID: {componentId}");
    }
}