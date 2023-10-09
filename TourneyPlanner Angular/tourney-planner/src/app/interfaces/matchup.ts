import { TeamsPerMatch } from "./teams-per-match";

export interface Matchup {
  id: number;
  round: number;
  nextMatchupId?: number;
  teamsPerMatch: TeamsPerMatch[];
}
