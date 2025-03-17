// filepath: c:\Users\Theophilus Kgopa\OneDrive\TRX_Auth\server\TRX_Auth.Tests\UserControllerTests.cs
using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers;
using server.Models;
using server.Repositories;
using System.Threading.Tasks;
using Xunit;

public class UserControllerTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _controller = new UserController(_mockRepo.Object);
    }

    [Fact]
    public async Task Register_ReturnsOkResult_WhenUserIsRegistered()
    {
        var registerModel = new RegisterModel
        {
            Name = "Alice",
            Surname = "Smith",
            IdOrPassport = "987654321",
            Contact = "987-654-3210",
            Email = "alice@example.com",
            Password = "stringst",
            ConfirmPassword = "stringst"
        };

        _mockRepo.Setup(repo => repo.UserExist(It.IsAny<string>())).ReturnsAsync(false);
        _mockRepo.Setup(repo => repo.Register(It.IsAny<UserModel>())).ReturnsAsync(new UserModel());

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Login_ReturnsOkResult_WhenCredentialsAreValid()
    {
        // Arrange
        var loginModel = new LoginModel
        {
            EmailOrId = "alice@example.com",
            Password = "stringst"
        };

        var userModel = new UserModel
        {
            Email = "alice@example.com",
            Password = "stringst"
        };

        _mockRepo.Setup(repo => repo.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(userModel);

        // Act
        var result = await _controller.Login(loginModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}