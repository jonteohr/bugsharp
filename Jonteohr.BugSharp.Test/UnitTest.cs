using BugSharp;

namespace Jonteohr.BugSharp.Test;

public class UnitTest
{
    private BugZilla _client;
    private const string BUGZILLA_URL = "";
    
    [SetUp]
    public void Setup()
    {
        _client = BugZilla.Create(BUGZILLA_URL);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}