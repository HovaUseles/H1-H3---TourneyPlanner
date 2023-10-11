import 'package:tourney_planner/src/models/tournament.dart';

enum TournamentStates {initial, loading, completed, error}

class TournamentCrudState {
  final TournamentStates _state;
  TournamentStates get currentState => _state;

  TournamentCrudState({required TournamentStates state}) : 
  _state = state;
}

class TournamentState extends TournamentCrudState {
  final TournamentDto? _tournament;

  TournamentDto? get tournament => _tournament;

  TournamentState({required super.state, TournamentDto? tournament})
      : _tournament = tournament;
}

class TournamentsState extends TournamentCrudState {
  final List<TournamentDto> _tournaments;

  List<TournamentDto> get tournaments => _tournaments;

  TournamentsState({required super.state, List<TournamentDto>? tournaments})
      : _tournaments = tournaments ?? [];
}