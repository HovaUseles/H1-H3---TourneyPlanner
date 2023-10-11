import 'package:get_it/get_it.dart';
import 'package:tourney_planner/src/datahandlers/auth_datahandler.dart';
import 'package:tourney_planner/src/datahandlers/team_datahandler.dart';
import 'package:tourney_planner/src/datahandlers/tournament_datahandler.dart';
import 'package:tourney_planner/src/datahandlers/user_datahandler.dart';

GetIt locator = GetIt.instance;

void setupLocators() async {
  locator.registerLazySingleton(() => TournamentDataHandler());
  locator.registerLazySingleton(() => TeamDataHandler());
  locator.registerLazySingleton(() => AuthDatahandler());
  locator.registerLazySingleton(() => UserDataHandler());
}