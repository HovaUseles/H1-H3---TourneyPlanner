import 'dart:convert';

class AuthDto {
  final String username;
  final String password;

  AuthDto({
    required this.username,
    required this.password,
  });
  
  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'username': username});
    result.addAll({'password': password});

    return result;
  }

  String toJson() => json.encode(toMap());
}