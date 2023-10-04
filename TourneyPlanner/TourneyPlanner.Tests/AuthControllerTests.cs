using Microsoft.AspNetCore.Mvc;
using Moq;
using TourneyPlanner.API.Controllers;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Repositories;

namespace TourneyPlanner.Tests
{
    public class AuthControllerTests
    {

        protected Mock<IUserRepository> GetUserRepoMock() { 
            return new Mock<IUserRepository>();
        }

        protected AuthController GetSut(Mock<IUserRepository> mock)
        {
            return new AuthController(mock.Object);
        }

        protected int GetStatusCode(ActionResult result)
        {
            return ((StatusCodeResult)result).StatusCode;
        }
        public class Register : AuthControllerTests
        {
            [Fact]
            public async Task ShouldReturn200_WhenSucceded()
            {
                // Arrange 
                var dto = new RegisterDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword"
                };
                var mock = GetUserRepoMock();
                var sut = GetSut(mock);

                // Act
                var result = await sut.Register(dto);

                // Assert
                Assert.Equal(200, GetStatusCode(result));
            }

            [Fact]
            public async Task ShouldCallUserRepository_WhenSucceded()
            {
                // Arrange 
                var dto = new RegisterDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword"
                };
                UserDto? userNotFound = null;

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userNotFound);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Register(dto);

                // Assert
                mock.Verify(x => x.GetByUsername(It.IsAny<string>()), Times.Once());
            }

            [Fact]
            public async Task ShouldReturn400_WhenUsernameAlreadyExists()
            {
                // Arrange 
                var dto = new RegisterDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword"
                };
                UserDto userFound = new UserDto()
                {
                    Id = 1,
                    Username = dto.Username
                };

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userFound);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Register(dto);

                // Assert
                Assert.Equal(400, GetStatusCode(result));
            }
        }
    }

}