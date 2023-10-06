using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using TourneyPlanner.API.Controllers;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Repositories;
using TourneyPlanner.API.Services;
using TourneyPlanner.Tests.Utilities;

namespace TourneyPlanner.Tests.UnitTests.ControllerTests
{
    public class AuthControllerTests
    {

        protected Mock<IUserRepository> GetUserRepoMock()
        {
            return new Mock<IUserRepository>();
        }

        protected Mock<ITokenService> GetTokenServiceMock()
        {
            TokenDto token = new TokenDto()
            {
                TokenString = Guid.NewGuid().ToString(),
                ExpiresIn = 60
            };
            var mock = new Mock<ITokenService>();
            mock.Setup(x => x.BuildNewToken(It.IsAny<UserDto>())).Returns(token);

            return mock;
        }

        protected AuthController GetSut(Mock<IUserRepository> mock)
        {
            var tokenMock = GetTokenServiceMock();

            return new AuthController(mock.Object, tokenMock.Object);
        }


        /// <summary>
        /// Test class for testing the Register method on the AuthController
        /// </summary>
        public class Register : AuthControllerTests
        {
            [Fact]
            public async Task ShouldReturn200_WhenSucceded()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };
                var mock = GetUserRepoMock();
                var sut = GetSut(mock);

                // Act
                var result = await sut.Register(dto);

                // Assert
                Assert.Equal(200, ResultHelper.GetStatusCode(result));
            }

            [Fact]
            public async Task ShouldCallUserRepository_WhenSucceded()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
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
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };
                UserDto userFound = new UserDto()
                {
                    Id = 1,
                    Email = dto.Username
                };

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userFound);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Register(dto);

                // Assert
                Assert.Equal(400, ResultHelper.GetStatusCode(result));
            }

            [Fact]
            public async Task ShouldCallUserRepositoryCreate_WhenUsernameUnique()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };
                UserDto? userNotFound = null;

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userNotFound);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Register(dto);

                // Assert
                mock.Verify(x => x.Create(It.IsAny<AuthHandlerDto>()), Times.Once());
            }

            [Theory]
            [InlineData(null, false)]
            [InlineData("", false)]
            [InlineData(" ", false)]
            [InlineData("password", false)]
            [InlineData("Password", false)]
            [InlineData("password1", false)]
            [InlineData("password!", false)]
            [InlineData("password1!", false)]
            [InlineData("Sh0rt", false)]
            [InlineData("P@ss", false)]
            [InlineData("Password1", true)]
            [InlineData("Password!", true)]

            public async Task ShouldReturnExpected_WhenPasswordIsNotStrongEnough(string password, bool expectSuccess)
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = password
                };
                UserDto? userNotFound = null;

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userNotFound);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Register(dto);

                // Assert
                if (expectSuccess)
                {
                    Assert.Equal(200, ResultHelper.GetStatusCode(result));
                }
                else
                {
                    Assert.Equal(400, ResultHelper.GetStatusCode(result));
                }
            }
        }

        /// <summary>
        /// Test class for testing the Login method on the AuthController
        /// </summary>
        public class Login : AuthControllerTests
        {
            [Fact]
            public async Task ShouldReturn200_WhenSucceded()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };
                UserDto? userFound = new UserDto
                {
                    Id = 1,
                    Email = dto.Username
                };

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userFound);
                mock.Setup(x => x.VerifyLogin(dto)).ReturnsAsync(true);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Login(dto);

                // Assert
                Assert.Equal(200, ResultHelper.GetStatusCode(result));
            }

            [Fact]
            public async Task ShouldCallUserRepository_WhenSucceded()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };

                UserDto? userFound = new UserDto
                {
                    Id = 1,
                    Email = dto.Username
                };

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userFound);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Login(dto);

                // Assert
                mock.Verify(x => x.GetByUsername(It.IsAny<string>()), Times.Once());
            }

            [Fact]
            public async Task ShouldReturn400_WhenUserDoesNotExists()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };

                UserDto? userNotFound = null;

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userNotFound);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Login(dto);

                // Assert
                Assert.Equal(400, ResultHelper.GetStatusCode(result));
            }

            [Fact]
            public async Task ShouldReturnToken_WhenSucceded()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };

                UserDto? userFound = new UserDto
                {
                    Id = 1,
                    Email = dto.Username
                };

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userFound);
                mock.Setup(x => x.VerifyLogin(dto)).ReturnsAsync(true);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Login(dto);
                var okObjectResult = result.Result as OkObjectResult;
                TokenDto? token = (TokenDto?)okObjectResult?.Value;

                // Assert
                Assert.True(!string.IsNullOrEmpty(token?.TokenString));
            }

            [Fact]
            public async Task ShouldCallUserRepository_ToValidateLogin()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };

                UserDto? userFound = new UserDto
                {
                    Id = 1,
                    Email = dto.Username
                };

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userFound);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Login(dto);

                // Assert
                mock.Verify(x => x.VerifyLogin(dto), Times.Once());
            }

            [Fact]
            public async Task ShouldReturn400_WhenPasswordIsInvalid()
            {
                // Arrange 
                var dto = new AuthHandlerDto()
                {
                    Username = "NewUser",
                    Password = "NewPassword1"
                };

                UserDto? userFound = new UserDto
                {
                    Id = 1,
                    Email = dto.Username
                };

                var mock = GetUserRepoMock();
                mock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(userFound);
                mock.Setup(x => x.VerifyLogin(dto)).ReturnsAsync(false);
                var sut = GetSut(mock);

                // Act
                var result = await sut.Login(dto);

                // Assert
                Assert.Equal(400, ResultHelper.GetStatusCode(result));
            }
        }
    }

}