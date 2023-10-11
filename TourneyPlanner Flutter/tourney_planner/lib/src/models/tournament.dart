import 'dart:convert';

import 'package:tourney_planner/src/models/matchup.dart';
import 'package:tourney_planner/src/models/user.dart';

class TournamentDto {
  final int id;
  final String name;
  final DateTime startDate;
  final String tournamentTypeName;
  final String gameTypeName;
  final UserDto createdBy;
  final List<MatchupDto> matchups;

  TournamentDto(
      {required this.id,
      required this.name,
      required this.startDate,
      required this.tournamentTypeName,
      required this.gameTypeName,
      required this.createdBy,
      required this.matchups});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'id': id});
    result.addAll({'name': name});
    result.addAll({'startDate': startDate});
    result.addAll({'tournamentType': tournamentTypeName});
    result.addAll({'gameType': gameTypeName});
    result.addAll({'createdBy': createdBy});
    result.addAll({'matchups': matchups});

    return result;
  }

  factory TournamentDto.fromMap(Map<String, dynamic> map) {
    return TournamentDto(
        id: map['id'] ?? 0,
        name: map['name'] ?? 'N/A',
        startDate: DateTime.parse(map['startDate'] ?? '01/01/1900'),
        tournamentTypeName: map['tournamentType'] ?? 'N/A',
        createdBy: UserDto.fromMap(map['createdBy']),
        gameTypeName: map['gameType'] ?? 'N/A',
        matchups: List<MatchupDto>.from(
            (map['matchups'] as List).map((i) => MatchupDto.fromJson(i))));
  }

  factory TournamentDto.fromJson(Map<String, dynamic> json) {
    int id = json['id'] ?? 0;
    String name = json['name'] ?? 'N/A';
    DateTime startDate = DateTime.parse(json['startDate'] ?? '01/01/1900');
    String tournamentTypeName = json['tournamentType'] ?? 'N/A';
    UserDto createdBy = UserDto.fromJson(json['createdBy']);
    String gameTypeName = json['gameType'] ?? 'N/A';
    List<MatchupDto> matchups = List<MatchupDto>.from(
        (json['matchups'] as List).map((i) => MatchupDto.fromJson(i)));

    return TournamentDto(
        id: id,
        name: name,
        startDate: startDate,
        tournamentTypeName: tournamentTypeName,
        createdBy: createdBy,
        gameTypeName: gameTypeName,
        matchups: matchups);
  }

  String toJson() => json.encode(toMap());
}
