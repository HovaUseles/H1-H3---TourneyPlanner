import 'dart:io';
import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:tourney_planner/src/datahandlers/base_datahandler.dart';
import 'package:tourney_planner/src/httpclient/set_http_client.dart';
import 'package:tourney_planner/src/models/auth.dart';

class AuthDatahandler extends BaseDataHandler {
  
  @override
  String get apiContext => "auth";
  // final baseUrl = 'https://10.0.2.2:7127/api/Auth';
  // HttpClient httpClient = HttpClient();
  
  Future<String> postLogin(AuthDto login) async {
    httpClient = await setHttpClient();
    var request = await httpClient.postUrl(Uri.parse('$baseUrl/login'))
      ..headers.add('Content-Type', 'application/json')
      ..add(
        utf8.encode(
          json.encode(
            login.toMap(),
          ),
        ),
      );
    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to login - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    String token = jsonDecode(responseBody)["token"];

    var storage = const FlutterSecureStorage();
    await storage.write(key: "token", value: token);

    return token;
  }

  Future<void> logout() async {
    var storage = const FlutterSecureStorage();
    await storage.deleteAll();
  }
}
