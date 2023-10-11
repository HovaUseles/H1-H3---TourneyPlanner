import 'dart:convert';

import 'package:tourney_planner/src/models/player.dart';

class TeamDto {
  final int id;
  final String name;
  final int score;
  final List<PlayerDto> players;

  TeamDto({required this.id, required this.name, required this.score, required this.players});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'type': id});
    result.addAll({'name': name});
    result.addAll({'score': score});
    result.addAll({'players': players});

    return result;
  }

  factory TeamDto.fromMap(Map<String, dynamic> map) {
    return TeamDto(
        // id: int.parse(map['id'] ?? 0),
        id: map['id'] ?? 0,
        name: map['name'] ?? 'N/A',
        score: map['score'] ?? 0,
        players: List<PlayerDto>.from(
            (map['players'] as List).map((i) => PlayerDto.fromJson(i))));
  }

  factory TeamDto.fromJson(Map<String, dynamic> json) {
    // int id = int.parse(json['id'] ?? 0);
    int id = json['id'] ?? 0;
    String name = json['name'] ?? 'N/A';
    int score = json['score'] ?? 0;
    List<PlayerDto> players = List<PlayerDto>.from(
        (json['players'] as List).map((i) => PlayerDto.fromJson(i)));

    return TeamDto(id: id, name: name, score: score, players: players);
  }

  String toJson() => json.encode(toMap());
}
