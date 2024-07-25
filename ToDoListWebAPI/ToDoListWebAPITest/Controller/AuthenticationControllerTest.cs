using Microsoft.Extensions.Configuration;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoListWebAPI.Authentication;
using ToDoListWebAPI.Controllers;
using ToDoListWebAPI.Models;


namespace ToDoListWebAPITest.Controller
{
    public class AuthenticationControllerTest
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationControllerTest() { 

            userManager= A.Fake<UserManager<ApplicationUser>>();
            roleManager = A.Fake<RoleManager<IdentityRole>>();
            _configuration = A.Fake<IConfiguration>();
        }

        [Fact]
        public async Task Login_ValidUser_ReturnsToken()
        {
            // Arrange
            var user = new ApplicationUser { UserName = "testuser" };
            var model = new LoginModel { Username = "testuser", Password = "password123" };
            var controller = new AuthenticationController(userManager, roleManager, _configuration);

            A.CallTo(() => userManager.FindByNameAsync(model.Username)).Returns(Task.FromResult(user));
            A.CallTo(() => userManager.CheckPasswordAsync(user, model.Password)).Returns(Task.FromResult(true));
            A.CallTo(() => userManager.GetRolesAsync(user)).Returns(Task.FromResult((IList<string>)new List<string> { "User" }));
            A.CallTo(() => _configuration["JWT:Secret"]).Returns("my_secret_key_12345");
            A.CallTo(() => _configuration["JWT:ValidIssuer"]).Returns("http://localhost:5000");
            A.CallTo(() => _configuration["JWT:ValidAudience"]).Returns("http://localhost:5000");

            // Act
            var result = await controller.Login(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tokenResult = okResult.Value as dynamic;
            Assert.NotNull(tokenResult);
            Assert.NotNull(tokenResult.token);
            Assert.NotNull(tokenResult.expiration);
        }

        [Fact]
        public async Task Register_NewUser_ReturnsSuccess()
        {
            // Arrange
            var model = new RegisterModel { Username = "newuser", Password = "password123", Email = "newuser@example.com" };
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email, SecurityStamp = "randomStamp" };
            var controller = new AuthenticationController(userManager, roleManager, _configuration);

            A.CallTo(() => userManager.FindByNameAsync(model.Username)).Returns(Task.FromResult<ApplicationUser>(null));
            A.CallTo(() => userManager.CreateAsync(A<ApplicationUser>.Ignored, model.Password)).Returns(IdentityResult.Success);

            // Act
            var result = await controller.Register(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as Response;
            Assert.Equal("Success", response.Status);
            Assert.Equal("User created successfully", response.Message);
        }

    }
}
