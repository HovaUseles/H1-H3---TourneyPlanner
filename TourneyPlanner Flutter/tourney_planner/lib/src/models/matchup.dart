import 'dart:convert';
import 'package:tourney_planner/src/models/teams_per_match.dart';

class MatchupDto {
  final int id;
  final int round;
  final int? nextMatchupId;
  final List<TeamsPerMatchDto> teamsPerMatch;

  MatchupDto(
      {required this.id,
      required this.round,
      required this.nextMatchupId,
      required this.teamsPerMatch});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'type': id});
    result.addAll({'round': round});
    result.addAll({'nextMatchupId': nextMatchupId});
    result.addAll({'teamsPerMatch': teamsPerMatch});

    return result;
  }

  factory MatchupDto.fromMap(Map<String, dynamic> map) {
    return MatchupDto(
      id: int.parse(map['id'] ?? 0),
      round: int.parse(map['round'] ?? 0),
      nextMatchupId: int.parse(map['nextMatchupId'] ?? 0),
      teamsPerMatch: List<TeamsPerMatchDto>.from((map['teamsPerMatch'] as List)
          .map((i) => TeamsPerMatchDto.fromJson(i))),
    );
  }

  factory MatchupDto.fromJson(Map<String, dynamic> json) {
    int id = int.parse(json['id'] ?? 0);
    int round = int.parse(json['round'] ?? 0);
    int nextMatchupId = int.parse(json['startDate'] ?? 0);
    List<TeamsPerMatchDto> teamsPerMatch = List<TeamsPerMatchDto>.from(
        (json['teams'] as List).map((i) => TeamsPerMatchDto.fromJson(i)));

    return MatchupDto(
        id: id,
        round: round,
        nextMatchupId: nextMatchupId,
        teamsPerMatch: teamsPerMatch);
  }

  String toJson() => json.encode(toMap());
}
