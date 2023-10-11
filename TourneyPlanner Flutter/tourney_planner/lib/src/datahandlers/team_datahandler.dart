import 'dart:io';
import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:tourney_planner/src/datahandlers/base_datahandler.dart';
import 'package:tourney_planner/src/httpclient/set_http_client.dart';
import 'package:tourney_planner/src/models/team.dart';

class TeamDataHandler extends BaseDataHandler{

  @override
  String get apiContext => "Team";

  Future<TeamDto> getTeam(int id) async {
    // var storage = const FlutterSecureStorage();
    // String? token = await storage.read(key: "token");

    // if (token == null) {
    //   throw Exception('[ERROR] No token in secure storage');
    // }

    // var headers = {"Authorization": "Bearer $token"};
    httpClient = await setHttpClient();
    HttpClientRequest request =
        await httpClient.getUrl(Uri.parse("${baseUrl}/${id}"))
          ..headers.contentType =
              ContentType('application', 'json', charset: 'utf-8');
          // ..headers.add(headers.keys.first, headers.values.first);

    var response = await request.close();

    if (response.statusCode != 200) {
      throw Exception(
          '[ERROR] Failed to post - response code: ${response.statusCode}');
    }

    var responseBody = await response.transform(utf8.decoder).join();

    TeamDto tempTeam = TeamDto.fromJson(json.decode(responseBody));
    // List<TournamentDto> tempCollection = [];

    // for (var item in json.decode(responseBody)) {
    //   TournamentDto card = TournamentDto.fromJson(item);
    //   tempCollection.add(card);
    // }

    return tempTeam;
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