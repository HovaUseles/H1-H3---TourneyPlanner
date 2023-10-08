import 'dart:convert';

class PlayerDto {
  final int id;
  final String firstName;
  final String? lastName;
  final int teamId;

  PlayerDto(
      {required this.id, required this.firstName, required this.lastName, required this.teamId});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'type': id});
    result.addAll({'firstName': firstName});
    result.addAll({'lastName': lastName});
    result.addAll({'teamId': teamId});

    return result;
  }

  factory PlayerDto.fromMap(Map<String, dynamic> map) {
    return PlayerDto(
        id: int.parse(map['id'] ?? 0),
        firstName: map['firstName'] ?? 'N/A',
        lastName: map['lastName'] ?? 'N/A',
        teamId: int.parse(map['lastName'] ?? 0));
  }

  factory PlayerDto.fromJson(Map<String, dynamic> json) {
    int id = int.parse(json['id'] ?? 0);
    String firstName = json['firstName'] ?? 'N/A';
    String lastName = json['lastName'] ?? 'N/A';
    int teamId = int.parse(json['lastName'] ?? 0);

    return PlayerDto(id: id, firstName: firstName, lastName: lastName, teamId: teamId);
  }

  String toJson() => json.encode(toMap());
}
