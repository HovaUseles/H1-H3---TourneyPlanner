import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:tourney_planner/src/datahandlers/tournament_datahandler.dart';
import 'package:tourney_planner/src/events/tournament_event.dart';
import 'package:tourney_planner/src/locators/setup_locator.dart';
import 'package:tourney_planner/src/states/tournament_state.dart';

class TournamentBloc extends Bloc<TournamentEvent, TournamentState> {
  TournamentBloc() : super(TournamentState(state: TournamentStates.initial)) {
    on<TournamentGetListEvent>(_getListEvent);
    on<TournamentGetMyListEvent>(_getMyListEvent);
    on<TournamentCreateEvent>(_createTournamentEvent);
  }

  void _getListEvent(TournamentGetListEvent event, Emitter<TournamentState> emit) async {
    emit(TournamentState(state: TournamentStates.loading));
    final apiService = locator<TournamentDataHandler>();

    try {
      await apiService.getTournamentCollection();
    } catch (e) {
      emit(TournamentState(state: TournamentStates.error));
    }
  }

    void _getMyListEvent(TournamentGetMyListEvent event, Emitter<TournamentState> emit) async {
    emit(TournamentState(state: TournamentStates.loading));
    final apiService = locator<TournamentDataHandler>();

    try {
      await apiService.getMyTournamentCollection(event.id);
    } catch (e) {
      emit(TournamentState(state: TournamentStates.error));
    }
  }

  void _createTournamentEvent(TournamentCreateEvent event, Emitter<TournamentState> emit) async {
    emit(TournamentState(state: TournamentStates.loading));
    final apiService = locator<TournamentDataHandler>();

    try {
      await apiService.postTournament(event.tournament);
    } catch (e) {
      emit(TournamentState(state: TournamentStates.error));
    }
  }
}
