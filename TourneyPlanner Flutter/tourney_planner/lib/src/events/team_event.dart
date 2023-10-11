import 'package:tourney_planner/src/models/team.dart';

abstract class TeamEvent {}

class TeamGetEvent implements TeamEvent {
  final int _id;

  int get id => _id;

  TeamGetEvent(this._id);
}

class TeamUpdateEvent implements TeamEvent {
  final TeamDto _team;

  TeamDto get team => _team;

  TeamUpdateEvent(this._team);
}