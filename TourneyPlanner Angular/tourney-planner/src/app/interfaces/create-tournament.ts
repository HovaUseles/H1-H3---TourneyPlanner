import { Team } from "./team";

export interface CreateTournament {
  name: string;
  tournamentTypeId: number;
  gameTypeId: number;
  startDate: Date;
  randomnizeTeams: boolean;
  teams: Team[];
}
