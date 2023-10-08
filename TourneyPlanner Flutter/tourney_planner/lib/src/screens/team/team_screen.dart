import 'package:flutter/material.dart';
import 'package:tourney_planner/src/models/player.dart';
import 'package:tourney_planner/src/models/team.dart';

class TeamScreen extends StatefulWidget {
  const TeamScreen({super.key});

  @override
  State<TeamScreen> createState() => _TeamScreenState();
}

class _TeamScreenState extends State<TeamScreen> {
  @override
  Widget build(BuildContext context) {
    List<PlayerDto> players1 = [
      PlayerDto(id: 1, firstName: 'fTest1', lastName: 'lTest1', teamId: 1),
    ];
    List<PlayerDto> players2 = [
      PlayerDto(id: 2, firstName: 'fTest1', lastName: 'lTest1', teamId: 2),
    ];
    List<PlayerDto> players3 = [
      PlayerDto(id: 3, firstName: 'fTest1', lastName: 'lTest1', teamId: 3),
    ];
    List<PlayerDto> players4 = [
      PlayerDto(id: 4, firstName: 'fTest1', lastName: 'lTest1', teamId: 4),
    ];
    List<PlayerDto> players5 = [
      PlayerDto(id: 5, firstName: 'fTest1', lastName: 'lTest1', teamId: 5),
    ];
    List<PlayerDto> players6 = [
      PlayerDto(id: 6, firstName: 'fTest1', lastName: 'lTest1', teamId: 6),
    ];
    List<PlayerDto> players7 = [
      PlayerDto(id: 7, firstName: 'fTest1', lastName: 'lTest1', teamId: 7),
    ];
    List<PlayerDto> players8 = [
      PlayerDto(id: 8, firstName: 'fTest1', lastName: 'lTest1', teamId: 8),
    ];
    List<TeamDto> matchup1 = [
      TeamDto(id: 1, name: 'Team1', players: players1),
      TeamDto(id: 2, name: 'Team2', players: players2)
    ];
    List<TeamDto> matchup2 = [
      TeamDto(id: 3, name: 'Team3', players: players3),
      TeamDto(id: 4, name: 'Team4', players: players4)
    ];
    List<TeamDto> matchup3 = [
      TeamDto(id: 5, name: 'Team5', players: players5),
      TeamDto(id: 6, name: 'Team6', players: players6)
    ];
    List<TeamDto> matchup4 = [
      TeamDto(id: 7, name: 'Team7', players: players7),
      TeamDto(id: 8, name: 'Team8', players: players8)
    ];
    List<TeamDto> matchup5 = [
      TeamDto(id: 2, name: 'Team2', players: players2),
      TeamDto(id: 4, name: 'Team4', players: players4)
    ];
    List<TeamDto> matchup6 = [
      TeamDto(id: 5, name: 'Team5', players: players5),
      TeamDto(id: 8, name: 'Team8', players: players8)
    ];
    List<TeamDto> matchup7 = [
      TeamDto(id: 4, name: 'Team4', players: players4),
      TeamDto(id: 8, name: 'Team8', players: players8)
    ];
    return const Placeholder();
  }
}
