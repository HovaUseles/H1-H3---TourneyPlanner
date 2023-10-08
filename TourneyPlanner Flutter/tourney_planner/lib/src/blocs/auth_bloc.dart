import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:tourney_planner/src/datahandlers/auth_datahandler.dart';
import 'package:tourney_planner/src/events/auth_event.dart';
import 'package:tourney_planner/src/locators/setup_locator.dart';
import 'package:tourney_planner/src/states/auth_state.dart';

class AuthBloc extends Bloc<AuthEvent, AuthState> {
  AuthBloc() : super(AuthState(state: AuthStates.initial)) {
    on<AuthLoginEvent>(_authLoginEvent);
    on<AuthLogoutEvent>(_authLogoutEvent);
  }

  void _authLoginEvent(AuthLoginEvent event, Emitter<AuthState> emit) async {
    emit(AuthState(state: AuthStates.loading));
    final apiService = locator<AuthDatahandler>();

    try {
      await apiService.postLogin(event.auth);
    } catch (e) {
      emit(AuthState(state: AuthStates.error));
    }
  }

  void _authLogoutEvent(AuthLogoutEvent event, Emitter<AuthState> emit) async {
    emit(AuthState(state: AuthStates.loading));
    final apiService = locator<AuthDatahandler>();

    try {
      await apiService.logout();
    } catch (e) {
      emit(AuthState(state: AuthStates.error));
    }
  }
}
