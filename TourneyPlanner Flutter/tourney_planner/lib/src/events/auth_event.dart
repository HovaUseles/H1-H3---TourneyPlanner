import 'package:tourney_planner/src/models/auth.dart';

abstract class AuthEvent {}

class AuthLoginEvent implements AuthEvent {
  final AuthDto _auth;

  AuthDto get auth => _auth;

  AuthLoginEvent(this._auth);
}

class AuthLogoutEvent implements AuthEvent {
  final AuthDto _auth;

  AuthDto get auth => _auth;

  AuthLogoutEvent(this._auth);
}