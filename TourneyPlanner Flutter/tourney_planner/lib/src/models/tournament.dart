import 'dart:convert';

import 'package:tourney_planner/src/models/matchup.dart';

class TournamentDto {
  final int id;
  final String name;
  final DateTime startDate;
  final String tournamentTypeName;
  final int tournamentTypeId;
  final String gameTypeName;
  final int gameTypeId;
  final String createdByName;
  final int createdById;
  final List<MatchupDto> matchups;

  TournamentDto(
      {required this.id,
      required this.name,
      required this.startDate,
      required this.tournamentTypeName,
      required this.tournamentTypeId,
      required this.gameTypeName,
      required this.gameTypeId,
      required this.createdByName,
      required this.createdById,
      required this.matchups});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'id': id});
    result.addAll({'name': name});
    result.addAll({'startDate': startDate});
    result.addAll({'tournamentTypeName': tournamentTypeName});
    result.addAll({'tournamentTypeId': tournamentTypeId});
    result.addAll({'gameTypeName': gameTypeName});
    result.addAll({'gameTypeId': gameTypeId});
    result.addAll({'createdByName': createdByName});
    result.addAll({'createdById': createdById});
    result.addAll({'matchups': matchups});

    return result;
  }

  factory TournamentDto.fromMap(Map<String, dynamic> map) {
    return TournamentDto(
        id: int.parse(map['id'] ?? 0),
        name: map['name'] ?? 'N/A',
        startDate: DateTime.parse(map['startDate'] ?? '01/01/1900'),
        tournamentTypeName: map['tournamentTypeName'] ?? 'N/A',
        tournamentTypeId: int.parse(map['tournamentTypeId'] ?? 0),
        createdByName: map['createdByName'] ?? 'N/A',
        createdById: int.parse(map['createdById'] ?? 0),
        gameTypeName: map['gameTypeName'] ?? 'N/A',
        gameTypeId: int.parse(map['gameTypeId'] ?? 0),
        matchups: List<MatchupDto>.from(
            (map['teams'] as List).map((i) => MatchupDto.fromJson(i))));
  }

  factory TournamentDto.fromJson(Map<String, dynamic> json) {
    int id = int.parse(json['id'] ?? 0);
    String name = json['name'] ?? 'N/A';
    DateTime startDate = DateTime.parse(json['startDate'] ?? '01/01/1900');
    String tournamentTypeName = json['tournamentTypeName'] ?? 'N/A';
    int tournamentTypeId = int.parse(json['tournamentTypeId'] ?? 0);
    String createdByName = json['createdByName'] ?? 'N/A';
    int createdById = int.parse(json['createdById'] ?? 0);
    String gameTypeName = json['gameTypeName'] ?? 'N/A';
    int gameTypeId = int.parse(json['gameTypeId'] ?? 0);
    List<MatchupDto> matchups = List<MatchupDto>.from(
        (json['teams'] as List).map((i) => MatchupDto.fromJson(i)));

    return TournamentDto(
        id: id,
        name: name,
        startDate: startDate,
        tournamentTypeName: tournamentTypeName,
        tournamentTypeId: tournamentTypeId,
        createdByName: createdByName,
        createdById: createdById,
        gameTypeName: gameTypeName,
        gameTypeId: gameTypeId,
        matchups: matchups);
  }

  String toJson() => json.encode(toMap());
}
