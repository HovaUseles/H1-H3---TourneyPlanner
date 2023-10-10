import 'package:tourney_planner/src/models/auth.dart';

abstract class UserEvent {}

class UserRegisterEvent implements UserEvent {
  final AuthDto _user;

  AuthDto get user => _user;

  UserRegisterEvent(this._user);
}

class UserChangePasswordEvent implements UserEvent {
  final AuthDto _user;

  AuthDto get user => _user;

  UserChangePasswordEvent(this._user);
}

class UserChangeEmailEvent implements UserEvent {
  final AuthDto _user;

  AuthDto get user => _user;

  UserChangeEmailEvent(this._user);
}