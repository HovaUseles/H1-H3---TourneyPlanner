import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:tourney_planner/src/datahandlers/tournament_datahandler.dart';
import 'package:tourney_planner/src/events/tournament_event.dart';
import 'package:tourney_planner/src/locators/setup_locator.dart';
import 'package:tourney_planner/src/models/tournament.dart';
import 'package:tourney_planner/src/states/tournament_state.dart';

class TournamentBloc extends Bloc<TournamentEvent, TournamentCrudState> {
  TournamentBloc() : super(TournamentCrudState(state: TournamentStates.initial)) {
    on<TournamentGetListEvent>(_getListEvent);
    on<TournamentGetByIdEvent>(_getByIdEvent);
    on<TournamentGetMyListEvent>(_getMyListEvent);
    on<TournamentCreateEvent>(_createTournamentEvent);
  }

  void _getByIdEvent(TournamentGetByIdEvent event, Emitter<TournamentCrudState> emit) async {
    emit(TournamentState(state: TournamentStates.loading));
    final apiService = locator<TournamentDataHandler>();

    try {
      TournamentDto tournament = await apiService.getTournamentById(event.id);
      emit(TournamentState(state: TournamentStates.completed, tournament: tournament));
    } catch (e) {
      emit(TournamentState(state: TournamentStates.error));
    }
  }
  void _getListEvent(TournamentGetListEvent event, Emitter<TournamentCrudState> emit) async {
    emit(TournamentsState(state: TournamentStates.loading));
    final apiService = locator<TournamentDataHandler>();

    try {
      List<TournamentDto> tournaments = await apiService.getTournamentCollection();
      emit(TournamentsState(state: TournamentStates.completed, tournaments: tournaments));
    } catch (e) {
      emit(TournamentsState(state: TournamentStates.error));
    }
  }

    void _getMyListEvent(TournamentGetMyListEvent event, Emitter<TournamentCrudState> emit) async {
    emit(TournamentState(state: TournamentStates.loading));
    final apiService = locator<TournamentDataHandler>();

    try {
      await apiService.getMyTournamentCollection(event.id);
    } catch (e) {
      emit(TournamentState(state: TournamentStates.error));
    }
  }

  void _createTournamentEvent(TournamentCreateEvent event, Emitter<TournamentCrudState> emit) async {
    emit(TournamentState(state: TournamentStates.loading));
    final apiService = locator<TournamentDataHandler>();

    try {
      await apiService.postTournament(event.tournament);
    } catch (e) {
      emit(TournamentState(state: TournamentStates.error));
    }
  }
}
