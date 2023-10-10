import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:tourney_planner/src/datahandlers/user_datahandler.dart';
import 'package:tourney_planner/src/events/user_event.dart';
import 'package:tourney_planner/src/locators/setup_locator.dart';
import 'package:tourney_planner/src/states/user_state.dart';

class UserBloc extends Bloc<UserEvent, UserState> {
  UserBloc() : super(UserState(state: UserStates.initial)) {
    on<UserRegisterEvent>(_userRegisterEvent);
    on<UserChangePasswordEvent>(_userChangePasswordEvent);
    on<UserChangeEmailEvent>(_userChangeEmailEvent);
  }

  void _userRegisterEvent(UserRegisterEvent event, Emitter<UserState> emit) async {
    emit(UserState(state: UserStates.loading));
    final apiService = locator<UserDataHandler>();

    try {
      await apiService.postUserRegistration(event.user);
    } catch (e) {
      emit(UserState(state: UserStates.error));
    }
  }

  void _userChangePasswordEvent(UserChangePasswordEvent event, Emitter<UserState> emit) async {
    emit(UserState(state: UserStates.loading));
    final apiService = locator<UserDataHandler>();

    try {
      await apiService.postChangePassword(event.user);
    } catch (e) {
      emit(UserState(state: UserStates.error));
    }
  }

    void _userChangeEmailEvent(UserChangeEmailEvent event, Emitter<UserState> emit) async {
    emit(UserState(state: UserStates.loading));
    final apiService = locator<UserDataHandler>();

    try {
      await apiService.postChangeEmail(event.user);
    } catch (e) {
      emit(UserState(state: UserStates.error));
    }
  }
}
