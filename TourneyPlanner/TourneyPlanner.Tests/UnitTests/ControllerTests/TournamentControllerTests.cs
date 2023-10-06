using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text.Json;
using TourneyPlanner.API.Controllers;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;
using TourneyPlanner.API.Repositories;
using TourneyPlanner.Tests.Utilities;

namespace TourneyPlanner.Tests.UnitTests.ControllerTests
{
    public class TournamentControllerTests
    {
        protected Mock<ITournamentRepository> GetTournamentRepoMock()
        {
            return new Mock<ITournamentRepository>();
        }

        protected TournamentController GetSut(Mock<ITournamentRepository> tournamentRepoMock)
        {
            return new TournamentController(tournamentRepoMock.Object);
        }

        protected CreateTournamentDto GetMockCreateTournamentDto()
        {
            return new CreateTournamentDto
            {
                Name = "Tournament 1",
                StartDate = DateTime.Parse("17/10/2022"),
                GameType = "Paddle",
                Type = new TournamentType
                {
                    Id = 1,
                    Name = "Knockout"
                },
                RandomnizeTeams = false,
                Teams = new[] {
                    new TeamDto
                    {
                        Id = 1,
                        Score = 0,
                        TeamName = "Team " + 1,
                        Players = null
                    },
                    new TeamDto
                    {
                        Id = 2,
                        Score = 0,
                        TeamName = "Team " + 2,
                        Players = null
                    },
                }
            };
        }

        protected IEnumerable<TournamentDto> GetMockTournaments()
        {
            int matchupIdCounter = 1;
            int teamIdCounter = 1;

            TeamDto[] teams = new TeamDto[4]
            {
                new TeamDto
                {
                    Id = teamIdCounter,
                    Score = 0,
                    TeamName = "Team " + teamIdCounter++,
                    Players = null
                },
                new TeamDto
                {
                    Id = teamIdCounter,
                    Score = 0,
                    TeamName = "Team " + teamIdCounter++,
                    Players = null
                },
                new TeamDto
                {
                    Id = teamIdCounter,
                    Score = 0,
                    TeamName = "Team " + teamIdCounter++,
                    Players = null
                },
                new TeamDto
                {
                    Id = teamIdCounter,
                    Score = 0,
                    TeamName = "Team " + teamIdCounter++,
                    Players = null
                }
            };


            List<TournamentDto> mockTournaments = new List<TournamentDto>{
                new TournamentDto()
                {
                    Id = 1,
                    Name = "Tournament 1",
                    StartDate = DateTime.Parse("17/10/2022"),
                    CreatedBy = new UserDto { Id = 1, Email = "TestUser" },
                    GameType = "Paddle",
                    Type = new TournamentType
                    {
                        Id = 1,
                        Name = "Knockout"
                    },
                    Matchups = new List<MatchupDto> {
                        new MatchupDto
                        {
                            Id = matchupIdCounter++,
                            Round = 2,
                            Teams = new List<TeamDto>
                            {
                                teams[0],
                                teams[3]
                            }
                        },
                        new MatchupDto
                        {
                            Id = matchupIdCounter++,
                            Round = 1,
                            Teams = new List<TeamDto>
                            {
                                teams[0],
                                teams[1]
                            },
                            WamId = 1,
                        },
                        new MatchupDto
                        {
                            Id = matchupIdCounter++,
                            Round = 1,
                            Teams = new List<TeamDto>
                            {
                                teams[2],
                                teams[3]
                            },
                            WamId = 1,
                        }
                    }
                },
                new TournamentDto()
                {
                    Id = 2,
                    Name = "Tournament 2",
                    StartDate = DateTime.Parse("22/8/2021"),
                    CreatedBy = new UserDto { Id = 1, Email = "TestUser" },
                    GameType = "Paddle",
                    Type = new TournamentType
                    {
                        Id = 1,
                        Name = "Knockout"
                    },
                    Matchups = new List<MatchupDto> {
                        new MatchupDto
                        {
                            Id = matchupIdCounter++,
                            Round = 2,
                            Teams = new List<TeamDto>
                            {
                                teams[0],
                                teams[2]
                            }
                        },
                        new MatchupDto
                        {
                            Id = matchupIdCounter++,
                            Round = 1,
                            Teams = new List<TeamDto>
                            {
                                teams[3],
                                teams[0]
                            },
                            WamId = 1,
                        },
                        new MatchupDto
                        {
                            Id = matchupIdCounter++,
                            Round = 1,
                            Teams = new List<TeamDto>
                            {
                                teams[2],
                                teams[1]
                            },
                            WamId = 4,
                        }
                    }
                },
            };

            return mockTournaments;
        }

        /// <summary>
        /// Test class for testing the GetAll Method in the TournamentController
        /// </summary>
        public class GetAll : TournamentControllerTests
        {
            [Fact]
            public async Task ShouldReturn200_WhenSucceeded()
            {
                // Arrange
                var repoMock = GetTournamentRepoMock();
                TournamentController sut = GetSut(repoMock);

                // Act
                var result = await sut.GetAll();

                // Assert
                Assert.Equal(200, ResultHelper.GetStatusCode(result));
            }

            [Fact]
            public async Task ShouldReturnTournamentDto_WhenSucceded()
            {
                // Arrange
                IEnumerable<TournamentDto> tournamentDtos = GetMockTournaments();

                var repoMock = GetTournamentRepoMock();
                repoMock.Setup(x => x.GetAll()).ReturnsAsync(tournamentDtos);
                TournamentController sut = GetSut(repoMock);

                // Act
                var result = await sut.GetAll();
                OkObjectResult okObjectResult = ((OkObjectResult)result.Result);
                IEnumerable<TournamentDto> actual = okObjectResult.Value as IEnumerable<TournamentDto>;
                IEnumerable<TournamentDto> expected = GetMockTournaments();

                string actualJson = JsonSerializer.Serialize(actual);
                string expectedJson = JsonSerializer.Serialize(expected);
                
                // Assert
                Assert.Equal(expected.Count(), actual.Count());
                Assert.Equal(expectedJson, actualJson);
            }
        }

        /// <summary>
        /// Test class for testing the GetById Method in the TournamentController
        /// </summary>
        public class GetById : TournamentControllerTests
        {

            [Fact]
            public async Task ShouldReturn200_WhenSucceeded()
            {
                // Arrange
                TournamentDto tournamentDto = GetMockTournaments().Where(t => t.Id == 1).First();

                var repoMock = GetTournamentRepoMock();
                repoMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(tournamentDto);
                TournamentController sut = GetSut(repoMock);

                // Act
                var result = await sut.GetById(1);

                // Assert
                Assert.Equal(200, ResultHelper.GetStatusCode(result));
            }

            [Fact]
            public async Task ShouldReturnTournamentDto_WhenSucceded()
            {
                // Arrange
                TournamentDto tournamentDto = GetMockTournaments().Where(t => t.Id == 1).First();

                var repoMock = GetTournamentRepoMock();
                repoMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(tournamentDto);
                TournamentController sut = GetSut(repoMock);

                // Act
                var result = await sut.GetById(1);
                OkObjectResult okObjectResult = ((OkObjectResult)result.Result);
                TournamentDto? actual = okObjectResult.Value as TournamentDto?;
                TournamentDto expected = tournamentDto;

                string actualJson = JsonSerializer.Serialize(actual);
                string expectedJson = JsonSerializer.Serialize(expected);

                // Assert
                Assert.Equal(expectedJson, actualJson);
            }
        }

        /// <summary>
        /// Test class for testing the Create Method in the TournamentController
        /// </summary>
        public class Create : TournamentControllerTests
        {

            [Fact]
            public async Task ShouldReturn201_WhenSucceeded()
            {
                // Arrange
                var repoMock = GetTournamentRepoMock();
                TournamentController sut = GetSut(repoMock);
                CreateTournamentDto createDto = GetMockCreateTournamentDto();

                // Act
                var result = await sut.Create(createDto);

                // Assert
                Assert.Equal(201, ResultHelper.GetStatusCode(result));
            }

            [Fact]
            public async Task ShouldReturnTournamentDto_WhenSucceded()
            {
                // Arrange
                TournamentDto tournamentDto = GetMockTournaments().Where(t => t.Id == 1).First();
                CreateTournamentDto createDto = GetMockCreateTournamentDto();

                var repoMock = GetTournamentRepoMock();
                repoMock.Setup(x => x.Create(createDto)).ReturnsAsync(tournamentDto);
                TournamentController sut = GetSut(repoMock);

                // Act
                var result = await sut.Create(createDto);
                CreatedResult createdResult = ((CreatedResult)result.Result);
                TournamentDto? actual = createdResult.Value as TournamentDto?;
                TournamentDto expected = tournamentDto;

                string actualJson = JsonSerializer.Serialize(actual);
                string expectedJson = JsonSerializer.Serialize(expected);

                // Assert
                Assert.Equal(expectedJson, actualJson);
            }
        }

        /// <summary>
        /// Test class for testing the Edit Method in the TournamentController
        /// </summary>
        public class Edit : TournamentControllerTests
        {

            [Fact]
            public async Task ShouldReturn204_WhenSucceeded()
            {
                // Arrange
                TournamentDto tournamentDto = GetMockTournaments().Where(t => t.Id == 1).First();
                var repoMock = GetTournamentRepoMock();
                TournamentController sut = GetSut(repoMock);
                CreateTournamentDto createDto = GetMockCreateTournamentDto();
                CreateTournamentDto updatedDto = createDto with
                {
                    Name = "Updated Tournament"
                };

                // Act
                var result = await sut.Edit(tournamentDto.Id, updatedDto);

                // Assert
                Assert.Equal(204, ResultHelper.GetStatusCode(result));
            }
        }

        /// <summary>
        /// Test class for testing the Delete Method in the TournamentController
        /// </summary>
        public class Delete : TournamentControllerTests
        {
            [Fact]
            public async Task ShouldReturn204_WhenSucceeded()
            {
                // Arrange
                TournamentDto tournamentDto = GetMockTournaments().Where(t => t.Id == 1).First();
                var repoMock = GetTournamentRepoMock();
                repoMock.Setup(x => x.GetById(tournamentDto.Id)).ReturnsAsync(tournamentDto);
                repoMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);
                TournamentController sut = GetSut(repoMock);

                // Act
                var result = await sut.Delete(tournamentDto.Id);

                // Assert
                Assert.Equal(204, ResultHelper.GetStatusCode(result));
            }
        }
    }
}
