import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:tourney_planner/src/datahandlers/team_datahandler.dart';
import 'package:tourney_planner/src/events/team_event.dart';
import 'package:tourney_planner/src/locators/setup_locator.dart';
import 'package:tourney_planner/src/models/team.dart';
import 'package:tourney_planner/src/states/team_state.dart';

class TeamBloc extends Bloc<TeamEvent, TeamState> {
  TeamBloc() : super(TeamState(state: TeamStates.initial)) {
    on<TeamGetEvent>(_teamGetEvent);
    on<TeamUpdateEvent>(_teamUpdateEvent);
  }

  void _teamGetEvent(TeamGetEvent event, Emitter<TeamState> emit) async {
    emit(TeamState(state: TeamStates.loading));
    final apiService = locator<TeamDataHandler>();

    try {
      TeamDto team = await apiService.getTeam(event.id);
      emit(TeamState(state: TeamStates.completed, team: team ));
    } catch (e) {
      emit(TeamState(state: TeamStates.error));
    }
  }

  void _teamUpdateEvent(TeamUpdateEvent event, Emitter<TeamState> emit) async {
    emit(TeamState(state: TeamStates.loading));
    final apiService = locator<TeamDataHandler>();

    try {
      await apiService.putTeam(event.team);
    } catch (e) {
      emit(TeamState(state: TeamStates.error));
    }
  }
}