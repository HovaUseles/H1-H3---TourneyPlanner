using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
                    StartDate = DateTime.Parse("17/10/2022"),
                    CreatedBy = new UserDto { Id = 1, Username = "TestUser" },
                    GameType = "Paddle",
                    Type = TournamentTypes.Knockout,
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
                    StartDate = DateTime.Parse("22/8/2021"),
                    CreatedBy = new UserDto { Id = 1, Username = "TestUser" },
                    GameType = "Paddle",
                    Type = TournamentTypes.Knockout,
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
    }
}
