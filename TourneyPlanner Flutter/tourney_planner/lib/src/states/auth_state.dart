import 'package:tourney_planner/src/models/auth.dart';

enum AuthStates {initial, loading, completed, error}

class AuthState {
  final AuthStates _state;
  final List<AuthDto> _auths;

  AuthStates get currentState => _state;
  List<AuthDto> get auths => _auths;

  AuthState({required AuthStates state, List<AuthDto>? auths})
      : _state = state,
        _auths = auths ?? [];
}