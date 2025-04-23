using BugSharp;
using BugSharp.Remote;
using Newtonsoft.Json;

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

    [Test]
    public void ToRemote_ShouldPopulate()
    {
        var bug = new Bug(_client)
        {
            Component = "component",
            Version = "version",
            AssignedTo = "user",
            Cc = [ "test_user" ],
            Classification = "classification",
            Created = new DateTime(2025, 4, 22),
            Creator = "creator",
            IsConfirmed = false,
            IsOpen = true,
            Changed = new DateTime(2025, 4, 23),
            Platform = "platform",
            Priority = "P1",
            Product = "product",
            QAContact = "qa_contact",
            Resolution = "resolution",
            Severity = "enhancement",
            Status = "UNCONFIRMED",
            Summary = "Bug summary",
            CustomFields = new Dictionary<string, object> { {"key", "a value"} },
            TargetMilestone = "target"
        };
        
        var remote = bug.ToRemoteBug();
        
        Assert.Multiple(() =>
        {
            Assert.That(remote.component, Is.EqualTo("component"));
            Assert.That(remote.version, Is.EqualTo("version"));
            Assert.That(remote.assigned_to, Is.EqualTo("user"));
            Assert.That(remote.cc.FirstOrDefault(), Is.EqualTo("test_user"));
            Assert.That(remote.classification, Is.EqualTo("classification"));
            Assert.That(remote.creation_time, Is.EqualTo(new DateTime(2025, 4, 22)));
            Assert.That(remote.is_confirmed, Is.False);
            Assert.That(remote.is_open, Is.True);
            Assert.That(remote.last_change_time, Is.EqualTo(new DateTime(2025, 4, 23)));
            Assert.That(remote.platform, Is.EqualTo("platform"));
            Assert.That(remote.priority, Is.EqualTo("P1"));
            Assert.That(remote.product, Is.EqualTo("product"));
            Assert.That(remote.qa_contact, Is.EqualTo("qa_contact"));
            Assert.That(remote.resolution, Is.EqualTo("resolution"));
            Assert.That(remote.severity, Is.EqualTo("enhancement"));
            Assert.That(remote.status, Is.EqualTo("UNCONFIRMED"));
            Assert.That(remote.summary, Is.EqualTo("Bug summary"));
            Assert.That(remote.CustomFields["key"], Is.EqualTo("a value"));
            Assert.That(remote.target_milestone, Is.EqualTo("target"));
        });
    }

    [Test]
    public void Bug_ShouldSerializeChangesOnly()
    {
        var bug = new Bug(_client);

        bug.Summary = "A new summary";

        var changes = JsonConvert.DeserializeObject<Dictionary<string, string>>(bug.SerializeChanges());
        
        Assert.That(changes, Is.Not.Null);
        Assert.That(changes, Has.Count.EqualTo(1));
        Assert.That(changes["summary"], Is.EqualTo("A new summary"));
    }

    [Test]
    public void Bug_RemoteShouldBeEqual()
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

        Assert.That(bug.CompareToRemote(), Has.Count.EqualTo(0));
    }

    [Test]
    public void BugSearch_ShouldSerialize()
    {
        var bugSearch = _client.CreateSearch();
        bugSearch.Status = "A status";
        bugSearch.Component = "component";
        bugSearch.QuickSearch = "status:unco";

        Assert.That(bugSearch.ToQueryString(), Is.EqualTo("component=component&status=A+status&quicksearch=status%3Aunco"));
    }
}