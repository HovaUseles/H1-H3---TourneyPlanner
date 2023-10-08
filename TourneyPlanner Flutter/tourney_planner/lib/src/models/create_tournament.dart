import 'dart:convert';

import 'package:tourney_planner/src/models/team.dart';

class CreateTournamentDto {
  final String tournamentType;
  final String gameType;
  final DateTime startDate;
  final bool randomize;
  final List<TeamDto> teams;

  CreateTournamentDto({
    required this.tournamentType,
    required this.gameType,
    required this.startDate,
    required this.randomize,
    required this.teams
  });

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'tournamentType': tournamentType});
    result.addAll({'gameType': gameType});
    result.addAll({'startDate': startDate});
    result.addAll({'randomize': randomize});
    result.addAll({'teams': teams});

    return result;
  }

  factory CreateTournamentDto.fromMap(Map<String, dynamic> map) {
    return CreateTournamentDto(
      tournamentType: map['tournamentType'] ?? 'N/A',
      gameType: map['gameType'] ?? 'N/A',
      startDate: DateTime.parse(map['startDate'] ?? '01/01/1900') ,
      randomize: bool.parse(map['randomize'] ?? 'false') ,
      teams: List<TeamDto>.from((map['teams'] as List).map((i) => TeamDto.fromJson(i)))
    );
  }

  factory CreateTournamentDto.fromJson(Map<String, dynamic> json) {
    String tournamentType = json['tournamentType'];
    String gameType = json['gameType'];
    DateTime startDate = json['startDate'];
    bool randomize = json['randomize'];
    List<TeamDto> teams = List<TeamDto>.from((json['teams'] as List).map((i) => TeamDto.fromJson(i)));

    return CreateTournamentDto(
        tournamentType: tournamentType,
        gameType: gameType,
        startDate: startDate,
        randomize: randomize,
        teams: teams);
  }

  String toJson() => json.encode(toMap());
}