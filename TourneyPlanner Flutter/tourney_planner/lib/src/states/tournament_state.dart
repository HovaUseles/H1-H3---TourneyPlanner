import 'package:tourney_planner/src/models/tournament.dart';

enum TournamentStates {initial, loading, completed, error}

class TournamentState {
  final TournamentStates _state;
  final List<TournamentDto> _tournaments;

  TournamentStates get currentState => _state;
  List<TournamentDto> get tournaments => _tournaments;

  TournamentState({required TournamentStates state, List<TournamentDto>? tournaments})
      : _state = state,
        _tournaments = tournaments ?? [];
}