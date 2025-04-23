using BugSharp;
using Moq;

namespace Jonteohr.BugSharp.Test;

public class TestableBugzilla : BugZilla
{
    public Mock<IBugService> BugService;
    
    public TestableBugzilla() : base(new ServiceLocator(), new BugZillaSettings(null, null))
    {
        BugService = new Mock<IBugService>();
        
        Services.Register<IBugService>(() => BugService.Object);
    }
}