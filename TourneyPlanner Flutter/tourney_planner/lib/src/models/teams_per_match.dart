import 'dart:convert';

class TeamsPerMatchDto {
  final int id;
  final int? score;
  final int matchupId;
  final int teamId;
  final String teamName;

  TeamsPerMatchDto(
      {required this.id, required this.score, required this.matchupId, required this.teamId, required this.teamName});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'type': id});
    result.addAll({'score': score});
    result.addAll({'matchupId': matchupId});
    result.addAll({'teamId': teamId});
    result.addAll({'teamName': teamName});

    return result;
  }

  factory TeamsPerMatchDto.fromMap(Map<String, dynamic> map) {
    return TeamsPerMatchDto(
        id: int.parse(map['id'] ?? 0),
        score: int.parse(map['score'] ?? 0),
        matchupId: int.parse(map['matchupId'] ?? 0),
        teamId: int.parse(map['lastName'] ?? 0),
        teamName: map['lastName'] ?? 'N/A');
  }

  factory TeamsPerMatchDto.fromJson(Map<String, dynamic> json) {
    int id = int.parse(json['id'] ?? 0);
    int score = int.parse(json['score'] ?? 0);
    int matchupId = int.parse(json['matchupId'] ?? 0);
    int teamId = int.parse(json['teamId'] ?? 0);
    String teamName = json['teamName'] ?? 'N/A';

    return TeamsPerMatchDto(id: id, score: score, matchupId: matchupId, teamId: teamId, teamName: teamName);
  }

  String toJson() => json.encode(toMap());
}
