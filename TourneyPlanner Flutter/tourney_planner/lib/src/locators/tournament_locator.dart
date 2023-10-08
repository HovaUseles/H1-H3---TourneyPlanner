import 'package:get_it/get_it.dart';
import 'package:tourney_planner/src/datahandlers/tournament_datahandler.dart';

GetIt locator = GetIt.instance;

void setupLocator() async {
  locator.registerLazySingleton(() => TournamentDataHandler());
}