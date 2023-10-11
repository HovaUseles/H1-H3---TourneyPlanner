import 'dart:io';
import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:tourney_planner/src/datahandlers/base_datahandler.dart';
import 'package:tourney_planner/src/httpclient/set_http_client.dart';
import 'package:tourney_planner/src/models/tournament.dart';

class TournamentDataHandler extends BaseDataHandler {

  @override
  String get apiContext => "Tournament";
  // final baseUrl = 'https://10.0.2.2:27016/api/Tournament';
  // HttpClient httpClient = HttpClient();

  Future<List<TournamentDto>> getTournamentCollection() async {
    httpClient = await setHttpClient();
    HttpClientRequest request =
        await httpClient.getUrl(Uri.parse(baseUrl))
          ..headers.contentType =
              ContentType('application', 'json', charset: 'utf-8');

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to post - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    List<TournamentDto> tempCollection = [];

    for (var item in json.decode(responseBody)) {
      TournamentDto card = TournamentDto.fromJson(item);
      tempCollection.add(card);
    }

    return tempCollection;
  }

  Future<List<TournamentDto>> getMyTournamentCollection(String id) async {
    var storage = const FlutterSecureStorage();
    String? token = await storage.read(key: "token");

    if (token == null) {
      throw Exception('[ERROR] No token in secure storage');
    }

    var headers = {"Authorization": "Bearer $token"};
    httpClient = await setHttpClient();
    HttpClientRequest request =
        await httpClient.getUrl(Uri.parse('$baseUrl/$id'))
          ..headers.contentType =
              ContentType('application', 'json', charset: 'utf-8')
          ..headers.add(headers.keys.first, headers.values.first);

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to post - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    List<TournamentDto> tempCollection = [];

    for (var item in json.decode(responseBody)) {
      TournamentDto card = TournamentDto.fromJson(item);
      tempCollection.add(card);
    }

    return tempCollection;
  }

  Future<TournamentDto> getTournamentById(int id) async {
    httpClient = await setHttpClient();
    HttpClientRequest request =
        await httpClient.getUrl(Uri.parse('$baseUrl/$id'))
          ..headers.contentType =
              ContentType('application', 'json', charset: 'utf-8');

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to post - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    TournamentDto tempTournament = TournamentDto.fromJson(json.decode(responseBody));

    return tempTournament;
  }

  Future<TournamentDto> postTournament(TournamentDto tournament) async {
    var storage = const FlutterSecureStorage();
    String? token = await storage.read(key: "token");

    if (token == null) {
      throw Exception('[ERROR] No token in secure storage');
    }

    var headers = {"Authorization": "Bearer $token"};
    httpClient = await setHttpClient();
    HttpClientRequest request =
        await httpClient.getUrl(Uri.parse(baseUrl))
          ..headers.contentType =
              ContentType('application', 'json', charset: 'utf-8')
          ..headers.add(headers.keys.first, headers.values.first);

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to post - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    TournamentDto tempCollection = TournamentDto.fromJson(json.decode(responseBody));

    return tempCollection;
  }
}