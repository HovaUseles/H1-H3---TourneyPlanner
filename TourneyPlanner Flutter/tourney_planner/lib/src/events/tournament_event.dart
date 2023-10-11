import 'package:tourney_planner/src/models/tournament.dart';

abstract class TournamentEvent {}

class TournamentGetListEvent implements TournamentEvent {}

class TournamentGetByIdEvent implements TournamentEvent {
  
  final int _id;

  int get id => _id;

  TournamentGetByIdEvent(this._id);
}

class TournamentGetMyListEvent implements TournamentEvent {
  final String _id;

  String get id => _id;

  TournamentGetMyListEvent(this._id);
}


class TournamentCreateEvent implements TournamentEvent {
  final TournamentDto _tournament;

  TournamentDto get tournament => _tournament;

  TournamentCreateEvent(this._tournament);
}

class TournamentUpdateEvent implements TournamentEvent {
  final TournamentDto _tournament;

  TournamentDto get tournament => _tournament;

  TournamentUpdateEvent(this._tournament);
}

class TournamentDeleteEvent implements TournamentEvent {
  final TournamentDto _tournament;

  TournamentDto get tournament => _tournament;

  TournamentDeleteEvent(this._tournament);
}