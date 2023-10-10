import 'package:get_it/get_it.dart';
import 'package:tourney_planner/src/datahandlers/auth_datahandler.dart';
import 'package:tourney_planner/src/datahandlers/tournament_datahandler.dart';
import 'package:tourney_planner/src/datahandlers/user_datahandler.dart';

GetIt locator = GetIt.instance;

void setupTournamentLocator() async {
  locator.registerLazySingleton(() => TournamentDataHandler());
}

void setupAuthLocator() async {
  locator.registerLazySingleton(() => AuthDatahandler());
}

void setupUserLocator() async {
  locator.registerLazySingleton(() => UserDataHandler());
}