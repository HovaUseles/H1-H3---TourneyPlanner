import { Player } from "./player";

export interface Team {
  id: number;
  teamName: string;
  score: number;
  players: Player[];
}
