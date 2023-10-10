enum UserStates {initial, loading, completed, error}

class UserState {
  final UserStates _state;

  UserStates get currentState => _state;

  UserState({required UserStates state})
      : _state = state;
}