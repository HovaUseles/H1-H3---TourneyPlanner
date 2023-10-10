import 'dart:convert';

class TokenDto {
  final String token;
  final int expiresIn;

  TokenDto({required this.token, required this.expiresIn});

  Map<String, dynamic> toMap() {
    final result = <String, dynamic>{};
    result.addAll({'token': token});
    result.addAll({'expiresIn': expiresIn});

    return result;
  }

  factory TokenDto.fromMap(Map<String, dynamic> map) {
    return TokenDto(
        token: map['token'] ?? 'N/A',
        expiresIn: int.parse(map['expiresIn'] ?? 0));
  }

  factory TokenDto.fromJson(Map<String, dynamic> json) {
    String token = json['token'] ?? 'N/A';
    int expiresIn = int.parse(json['expiresIn'] ?? 0);

    return TokenDto(token: token, expiresIn: expiresIn);
  }

  String toJson() => json.encode(toMap());
}
