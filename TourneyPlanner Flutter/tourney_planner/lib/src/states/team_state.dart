import 'package:tourney_planner/src/models/team.dart';

enum TeamStates { initial, loading, completed, error }

class TeamsState {
  final TeamStates _state;
  final List<TeamDto> _teams;

  TeamStates get currentState => _state;
  List<TeamDto> get teams => _teams;

  TeamsState({required TeamStates state, List<TeamDto>? teams})
      : _state = state,
        _teams = teams ?? [];
}

class TeamState {
  final TeamStates _state;
  final TeamDto? _team;

  TeamStates get currentState => _state;
  TeamDto? get team => _team;

  TeamState({required TeamStates state, TeamDto? team})
      : _state = state,
        _team = team;
}