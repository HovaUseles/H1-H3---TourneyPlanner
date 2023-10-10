import { Matchup } from "./matchup";

export interface Tournament {
  id: number;
  name: string;
  startDate: Date;
  gameType: string;
  type: string;
  matchups: Matchup[];
}
