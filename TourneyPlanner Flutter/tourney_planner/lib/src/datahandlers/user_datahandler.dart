import 'dart:io';
import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:tourney_planner/src/httpclient/set_http_client.dart';
import 'package:tourney_planner/src/models/auth.dart';

class UserDataHandler {
  final baseUrl = 'https://10.0.2.2:27016/api/Tournament';
  HttpClient httpClient = HttpClient();

  Future<bool> postUserRegistration(AuthDto user) async {
    httpClient = await setHttpClient();
    HttpClientRequest request = await httpClient.getUrl(Uri.parse(baseUrl))
      ..headers.contentType =
          ContentType('application', 'json', charset: 'utf-8');

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to post - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    return bool.parse(responseBody);
  }

  Future<bool> postChangePassword(AuthDto user) async {
    var storage = const FlutterSecureStorage();
    String? token = await storage.read(key: "token");

    if (token == null) {
      throw Exception('[ERROR] No token in secure storage');
    }

    var headers = {"Authorization": "Bearer $token"};
    httpClient = await setHttpClient();
    HttpClientRequest request = await httpClient.getUrl(Uri.parse(baseUrl))
      ..headers.contentType =
          ContentType('application', 'json', charset: 'utf-8')
      ..headers.add(headers.keys.first, headers.values.first);

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to post - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    return bool.parse(responseBody);
  }

  Future<bool> postChangeEmail(AuthDto user) async {
    var storage = const FlutterSecureStorage();
    String? token = await storage.read(key: "token");

    if (token == null) {
      throw Exception('[ERROR] No token in secure storage');
    }

    var headers = {"Authorization": "Bearer $token"};
    httpClient = await setHttpClient();
    HttpClientRequest request = await httpClient.getUrl(Uri.parse(baseUrl))
      ..headers.contentType =
          ContentType('application', 'json', charset: 'utf-8')
      ..headers.add(headers.keys.first, headers.values.first);

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to post - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    return bool.parse(responseBody);
  }
}
