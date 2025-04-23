using BugSharp;
using Moq;

namespace Jonteohr.BugSharp.Test;

public class TestableBugzilla : BugZilla
{
    public Mock<IBugService> BugService;
    public Mock<IAttachmentService> AttachmentService;
    public Mock<IComponentService> ComponentService;
    public Mock<IFieldService> FieldService;
    public Mock<IBugzillaInformation> BugzillaService;
    public Mock<ICommentService> CommentService;
    
    public TestableBugzilla() : base(new ServiceLocator(), new BugZillaSettings(null, null))
    {
        BugService = new Mock<IBugService>();
        AttachmentService = new Mock<IAttachmentService>();
        ComponentService = new Mock<IComponentService>();
        FieldService = new Mock<IFieldService>();
        BugzillaService = new Mock<IBugzillaInformation>();
        CommentService = new Mock<ICommentService>();
        
        Services.Register<IBugService>(() => BugService.Object);
        Services.Register<IAttachmentService>(() => AttachmentService.Object);
        Services.Register<IComponentService>(() => ComponentService.Object);
        Services.Register<IFieldService>(() => FieldService.Object);
        Services.Register<IBugzillaInformation>(() => BugzillaService.Object);
        Services.Register<ICommentService>(() => CommentService.Object);
    }

    public static TestableBugzilla Create()
    {
        return new TestableBugzilla();
    }
}