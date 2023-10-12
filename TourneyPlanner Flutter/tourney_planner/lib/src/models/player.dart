import 'dart:convert';

class PlayerDto {
  final int id;
  final String firstName;
  final String? lastName;

  PlayerDto(
      {required this.id, required this.firstName, required this.lastName});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'type': id});
    result.addAll({'firstName': firstName});
    result.addAll({'lastName': lastName});

    return result;
  }

  factory PlayerDto.fromMap(Map<String, dynamic> map) {
    return PlayerDto(
        // id: int.parse(map['id'] ?? 0),
        id: map['id'] ?? 0,
        firstName: map['firstName'] ?? 'N/A',
        lastName: map['lastName'] ?? 'N/A');
  }

  factory PlayerDto.fromJson(Map<String, dynamic> json) {
    // int id = int.parse(json['id'] ?? 0);
    int id = json['id'] ?? 0;
    String firstName = json['firstName'] ?? 'N/A';
    String lastName = json['lastName'] ?? 'N/A';

    return PlayerDto(id: id, firstName: firstName, lastName: lastName);
  }

  String toJson() => json.encode(toMap());
}
