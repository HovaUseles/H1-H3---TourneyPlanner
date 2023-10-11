import 'dart:convert';

class UserDto {
  final int id;
  final String email;

  UserDto({
    required this.id,
    required this.email,
  });
  
  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'id': id});
    result.addAll({'email': email});

    return result;
  }

  String toJson() => json.encode(toMap());

  factory UserDto.fromJson(Map<String, dynamic> json) {
    return UserDto.fromMap(json);
  }

  factory UserDto.fromMap(Map<String, dynamic> map) {
    return UserDto(
        id: map['id'] ?? 0,
        email: map['email'] ?? 'N/A',);
  }
}