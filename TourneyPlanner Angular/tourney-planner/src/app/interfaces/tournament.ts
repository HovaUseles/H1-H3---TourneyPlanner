import { Matchup } from "./matchup";

export interface Tournament {
  id: number;
  name: string;
  startDate: Date;
  tournamentTypeName: string;
  tournamentTypeId: number;
  gameTypeName: string;
  gameTypeId: number;
  matchups: Matchup[];
}
