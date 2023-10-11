import { Team } from "./team";

export interface Matchup {
  id: number;
  round: number;
  nextMatchupId?: number;
  teams: Team[];
}
