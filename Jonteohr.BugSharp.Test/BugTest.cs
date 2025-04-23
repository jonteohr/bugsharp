using BugSharp;
using BugSharp.Remote;

namespace Jonteohr.BugSharp.Test;

public class BugTest
{
    private TestableBugzilla _client;
    
    [SetUp]
    public void Setup()
    {
        _client = new TestableBugzilla();
    }

    [Test]
    public void FromRemote_ShouldPopulate()
    {
        var remotebug = new RemoteBug
        {
            component = "component",
            version = "version",
            assigned_to = "user",
            cc = [ "test_user" ],
            classification = "classification",
            creation_time = new DateTime(2025, 4, 22),
            creator = "creator",
            id = 1,
            is_confirmed = false,
            is_open = true,
            last_change_time = new DateTime(2025, 4, 23),
            platform = "platform",
            priority = "P1",
            product = "product",
            qa_contact = "qa_contact",
            resolution = "resolution",
            severity = "enhancement",
            status = "UNCONFIRMED",
            summary = "Bug summary",
            CustomFields = new Dictionary<string, object> { {"key", "a value"} },
            target_milestone = "target"
        };

        var bug = new Bug(_client, remotebug);
        
        Assert.Multiple(() =>
        {
            Assert.That(bug.Component, Is.EqualTo("component"));
            Assert.That(bug.Version, Is.EqualTo("version"));
            Assert.That(bug.AssignedTo, Is.EqualTo("user"));
            Assert.That(bug.Cc.FirstOrDefault(), Is.EqualTo("test_user"));
            Assert.That(bug.Classification, Is.EqualTo("classification"));
            Assert.That(bug.Created, Is.EqualTo(new DateTime(2025, 4, 22)));
            Assert.That(bug.Id, Is.EqualTo(1));
            Assert.That(bug.IsConfirmed, Is.False);
            Assert.That(bug.IsOpen, Is.True);
            Assert.That(bug.Changed, Is.EqualTo(new DateTime(2025, 4, 23)));
            Assert.That(bug.Platform, Is.EqualTo("platform"));
            Assert.That(bug.Priority, Is.EqualTo("P1"));
            Assert.That(bug.Product, Is.EqualTo("product"));
            Assert.That(bug.QAContact, Is.EqualTo("qa_contact"));
            Assert.That(bug.Resolution, Is.EqualTo("resolution"));
            Assert.That(bug.Severity, Is.EqualTo("enhancement"));
            Assert.That(bug.Status, Is.EqualTo("UNCONFIRMED"));
            Assert.That(bug.Summary, Is.EqualTo("Bug summary"));
            Assert.That(bug.CustomFields["key"], Is.EqualTo("a value"));
            Assert.That(bug.TargetMilestone, Is.EqualTo("target"));
        });
    }
}