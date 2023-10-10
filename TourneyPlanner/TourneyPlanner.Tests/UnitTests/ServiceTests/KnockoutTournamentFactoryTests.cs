using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Factories.Tournament;
using TourneyPlanner.API.Models;

namespace TourneyPlanner.Tests.UnitTests.ServiceTests
{
    public class KnockoutTournamentFactoryTests
    {
        protected KnockoutTournamentFactory GetSut()
        {
            return new KnockoutTournamentFactory();
        }

        protected CreateTournamentDto GetMockTournamentDto(IEnumerable<TeamDto> teams)
        {
            return new CreateTournamentDto
            {
                GameTypeId = 1,
                TournamentTypeId = 1,
                StartDate = DateTime.Now,
                Name = "Test",
                RandomnizeTeams = false,
                Teams = teams
            };
        }

        public class BuildMatchups : KnockoutTournamentFactoryTests 
        {
            [Theory]
            [InlineData(2, 1)]
            [InlineData(4, 3)]
            [InlineData(8, 7)]
            [InlineData(16, 15)]
            [InlineData(32, 31)]
            [InlineData(256, 255)]
            public async Task ShouldReturnExpectedNumberMatchups(int numberOfTeams, int expectedNumberOfMatchups)
            {
                // Arrange
                List<TeamDto> teams = new List<TeamDto>();
                for (int i = 0; i < numberOfTeams; i++)
                {
                    teams.Add(new TeamDto
                    {
                        TeamName = "Test " + i,
                        Score = 0,
                        Players = null
                    });
                }
                CreateTournamentDto dto = GetMockTournamentDto(teams);
                KnockoutTournamentFactory sut = GetSut();

                // Act 
                IEnumerable<Matchup> result = sut.BuildMatchups(dto);

                // Assert
                Assert.Equal(expectedNumberOfMatchups, result.Count());
            }

            [Theory]
            [InlineData(2)]
            [InlineData(4)]
            [InlineData(8)]
            [InlineData(16)]
            [InlineData(32)]
            public async Task ShouldReturnMatchupsWithNoDuplicateTeams(int numberOfTeams)
            {
                // Arrange
                List<TeamDto> teams = new List<TeamDto>();
                for (int i = 0; i < numberOfTeams; i++)
                {
                    teams.Add(new TeamDto
                    {
                        TeamName = "Test " + i,
                        Score = 0,
                        Players = null
                    });
                }
                CreateTournamentDto dto = GetMockTournamentDto(teams);
                KnockoutTournamentFactory sut = GetSut();

                // Act 
                IEnumerable<Matchup> result = sut.BuildMatchups(dto);
                List<string> matchupTeamNames = new List<string>();
                foreach (Matchup matchup in result.Where(m => m.Rounds == 1))
                {
                    matchupTeamNames.Add(matchup.MatchupTeams.First().Team.Name);
                    matchupTeamNames.Add(matchup.MatchupTeams.Last().Team.Name);
                }
                var matchupTeamNamesGroups = matchupTeamNames.GroupBy(mtn => mtn);

                // Assert
                Assert.False(matchupTeamNamesGroups.Any(mtng => mtng.Count() > 1));
            }

            [Theory]
            [InlineData(1)]
            [InlineData(5)]
            [InlineData(6)]
            [InlineData(20)]
            [InlineData(24)]
            public async Task ShouldThrow_WhenNumberOfTeamsIsInvalid(int numberOfTeams)
            {
                // Arrange
                List<TeamDto> teams = new List<TeamDto>();
                for (int i = 0; i < numberOfTeams; i++)
                {
                    teams.Add(new TeamDto
                    {
                        TeamName = "Test " + i,
                        Score = 0,
                        Players = null
                    });
                }
                CreateTournamentDto dto = GetMockTournamentDto(teams);
                KnockoutTournamentFactory sut = GetSut();

                // Act & Assert
                Assert.Throws<ArgumentException>(() => sut.BuildMatchups(dto));
            }

            [Fact]
            public async Task ShouldReturnMatchupsWithNextMatchup()
            {
                // Arrange
                List<TeamDto> teams = new List<TeamDto>();
                int numberOfTeams = 4;
                for (int i = 0; i < numberOfTeams; i++)
                {
                    teams.Add(new TeamDto
                    {
                        TeamName = "Test " + i,
                        Score = 0,
                        Players = null
                    });
                }
                CreateTournamentDto dto = GetMockTournamentDto(teams);
                KnockoutTournamentFactory sut = GetSut();

                // Act 
                IEnumerable<Matchup> result = sut.BuildMatchups(dto);
                Matchup final = result.First();
                Matchup semifinal1 = result.Where(m => m.Rounds == 1).First();
                Matchup semifinal2 = result.Where(m => m.Rounds == 1).Last();

                // Assert
                Assert.True(final.NextMatchup == null);
                Assert.Equal(semifinal1.NextMatchup, semifinal2.NextMatchup);
                Assert.Equal(final, semifinal1.NextMatchup);
                Assert.Equal(final, semifinal2.NextMatchup);
            }
        }
    }
}
