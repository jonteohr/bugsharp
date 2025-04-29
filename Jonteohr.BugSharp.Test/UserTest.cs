using BugSharp;
using Moq;

namespace Jonteohr.BugSharp.Test;

public class UserTest
{
    private TestableBugzilla _client;
    
    [SetUp]
    public void Setup()
    {
        _client = TestableBugzilla.Create();
    }

    [Test]
    public async Task Should_LoginAndLogout()
    {
        _client.UserService.Setup(service => service.Logout(It.IsAny<string>()));
        _client.UserService.Setup(service => service.Login(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new User(1, "username", "a-bbCCww"));
        
        var user = await _client.Users.Login("MyUser", "MyPassword");
        
        await _client.Users.Logout(user.Token);
    }
}