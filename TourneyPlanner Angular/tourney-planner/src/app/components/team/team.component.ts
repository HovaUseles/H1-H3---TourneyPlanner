import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { Player } from 'src/app/interfaces/player';
import { Team } from 'src/app/interfaces/team';
import { TeamService } from 'src/services/team.service';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {
  homeTeamPlayers: MatTableDataSource<Player> = new MatTableDataSource();
  awayTeamPlayers: MatTableDataSource<Player> = new MatTableDataSource();
  teams: Array<Team> = new Array();
  displayedColumns: Array<string> = ["Id", "First name", "Last name"];

  constructor(private teamService: TeamService) {
    this.teamService.teamsPerMatch$.subscribe(x => {
      this.homeTeamPlayers.data = x[0].players;
      this.awayTeamPlayers.data = x[1].players;
      this.teams = x;
    });
  }

  ngOnInit(): void {
  }


}
