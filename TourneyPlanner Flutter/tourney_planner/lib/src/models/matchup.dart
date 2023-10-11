import 'dart:convert';
import 'package:tourney_planner/src/models/team.dart';

class MatchupDto {
  final int id;
  final int round;
  final int? nextMatchupId;
  final List<TeamDto> teams;

  MatchupDto(
      {required this.id,
      required this.round,
      required this.nextMatchupId,
      required this.teams});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'type': id});
    result.addAll({'round': round});
    result.addAll({'nextMatchupId': nextMatchupId});
    result.addAll({'teamsPerMatch': teams});

    return result;
  }

  factory MatchupDto.fromMap(Map<String, dynamic> map) {
    return MatchupDto(
      id: map['id'] ?? 0,
      round: map['round'] ?? 0,
      nextMatchupId: map['nextMatchupId'],
      teams: List<TeamDto>.from((map['teams'] as List)
          .map((i) => TeamDto.fromJson(i))),
    );
  }

  factory MatchupDto.fromJson(Map<String, dynamic> json) {
    int id = json['id'] ?? 0;
    int round = json['round'] ?? 0;
    int? nextMatchupId = json['nextMatchupId'];
    List<TeamDto> teams = List<TeamDto>.from(
        (json['teams'] as List).map((i) => TeamDto.fromJson(i)));

    return MatchupDto(
        id: id,
        round: round,
        nextMatchupId: nextMatchupId,
        teams: teams);
  }

  String toJson() => json.encode(toMap());
}
