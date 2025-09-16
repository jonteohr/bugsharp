using BugSharp;
using BugSharp.Remote;
using Moq;
using Moq.Language.Flow;

namespace Jonteohr.BugSharp.Test;

public class CommentTest
{
    private TestableBugzilla _client;

    [SetUp]
    public void SetUp()
    {
        _client = TestableBugzilla.Create();
    }

    [Test]
    public void FromRemote_ShouldPopulate()
    {
        var remote = new RemoteComment
        {
            id = 1,
            creation_time = new DateTime(2025, 04, 23),
            creator = "creator",
            attachment_id = 2,
            bug_id = 123,
            count = 0,
            is_private = false,
            tags = ["tag1", "tag2"],
            text = "comment content",
            time = new DateTime(2025, 4, 23)
        };

        var comment = new Comment(_client, remote);
        
        Assert.Multiple(() =>
        {
            Assert.That(comment.Id, Is.EqualTo(1));
            Assert.That(comment.CreationTime, Is.EqualTo(new DateTime(2025, 04, 23)));
            Assert.That(comment.Creator, Is.EqualTo("creator"));
            Assert.That(comment.AttachmentId, Is.EqualTo(2));
            Assert.That(comment.BugId, Is.EqualTo(123));
            Assert.That(comment.Count, Is.EqualTo(0));
            Assert.That(comment.IsPrivate, Is.EqualTo(false));
            Assert.That(comment.Tags, Has.Count.EqualTo(2));
            Assert.That(comment.Tags[0], Is.EqualTo("tag1"));
            Assert.That(comment.Tags[1], Is.EqualTo("tag2"));
            Assert.That(comment.Text, Is.EqualTo("comment content"));
            Assert.That(comment.Time, Is.EqualTo(new DateTime(2025, 04, 23)));
        });
    }

    [Test]
    public async Task CreateComment()
    {
        var comment = new Comment(_client)
        {
            Text = "Comment content"
        };
        
        _client.CommentService.Setup(service => service.CreateCommentAsync(It.IsAny<Comment>()))
            .ReturnsAsync(1);
        
        var result = await _client.CommentService.Object.CreateCommentAsync(comment);
        
        Assert.That(result, Is.EqualTo(1));
    }
}