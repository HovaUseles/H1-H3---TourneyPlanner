import 'dart:io';

abstract class BaseDataHandler {
  String get baseUrl => '${_domain}/${apiContext}';
  final _domain = 'https://10.0.2.2:7127/api';

  String get apiContext;
  HttpClient httpClient = HttpClient();

}