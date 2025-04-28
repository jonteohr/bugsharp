namespace Example;

class Program
{
    public static async Task Main()
    {
        var exampleApp = new BugZillaExample();
        
        var bugInput = Console.ReadLine();
        if(!int.TryParse(bugInput, out var bugId))
            Console.WriteLine("Not a valid number");
        
        await exampleApp.DoBugzillaStuffAsync(bugId);
    }
}