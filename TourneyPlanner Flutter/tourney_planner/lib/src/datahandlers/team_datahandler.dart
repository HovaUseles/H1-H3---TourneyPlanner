import 'dart:io';
import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:tourney_planner/src/httpclient/set_http_client.dart';
import 'package:tourney_planner/src/models/team.dart';
import 'package:tourney_planner/src/models/tournament.dart';

class TeamDataHandler {
  final baseUrl = 'https://10.0.2.2:27016/api/Team';
  HttpClient httpClient = HttpClient();

  Future<List<TournamentDto>> getTeam(String id) async {
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

    List<TournamentDto> tempCollection = [];

    for (var item in json.decode(responseBody)) {
      TournamentDto card = TournamentDto.fromJson(item);
      tempCollection.add(card);
    }

    return tempCollection;
  }

  Future<TeamDto> putTeam(TeamDto team) async {
    var storage = const FlutterSecureStorage();
    String? token = await storage.read(key: "token");
    String id = team.id.toString();

    if (token == null) {
      throw Exception('[ERROR] No token in secure storage');
    }

    var headers = {"Authorization": "Bearer $token"};
    httpClient = await setHttpClient();
    HttpClientRequest request = await httpClient.putUrl(
      Uri.parse('$baseUrl/$id'),
    )
      ..headers.contentType =
          ContentType('application', 'json', charset: 'utf-8')
      ..headers.add(headers.keys.first, headers.values.first)
      ..add(
        utf8.encode(
          json.encode(
            team.toMap(),
          ),
        ),
      );

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to login - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    TeamDto scrumCard = TeamDto.fromJson(json.decode(responseBody));

    return scrumCard;
  }
}