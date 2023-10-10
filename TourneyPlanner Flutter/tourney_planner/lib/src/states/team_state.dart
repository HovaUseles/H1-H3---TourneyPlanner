import 'package:tourney_planner/src/models/team.dart';

enum TeamStates { initial, loading, completed, error }

class TeamState {
  final TeamStates _state;
  final List<TeamDto> _teams;

  TeamStates get currentState => _state;
  List<TeamDto> get scrumCards => _teams;

  TeamState({required TeamStates state, List<TeamDto>? teams})
      : _state = state,
        _teams = teams ?? [];
}