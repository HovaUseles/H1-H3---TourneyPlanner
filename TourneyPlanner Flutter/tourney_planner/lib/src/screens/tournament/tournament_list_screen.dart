import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:tourney_planner/src/blocs/tournament_bloc.dart';
import 'package:tourney_planner/src/events/tournament_event.dart';
import 'package:tourney_planner/src/models/tournament.dart';
import 'package:tourney_planner/src/screens/tournament/tournament_screen.dart';
import 'package:tourney_planner/src/screens/utility/custom_app_bar.dart';
import 'package:tourney_planner/src/states/tournament_state.dart';

class TournamentListScreen extends StatefulWidget {
  const TournamentListScreen({super.key});

  static const routeName = '/tournaments';

  @override
  State<TournamentListScreen> createState() => _TournamentListScreenState();
}

class _TournamentListScreenState extends State<TournamentListScreen> {
  List<TournamentDto> tournaments = [];
  @override
  Widget build(BuildContext context) {
    TournamentBloc tourneyBloc = context.read<TournamentBloc>();
    tourneyBloc.add(TournamentGetListEvent());
    return Scaffold(
      appBar: customAppBar(context: context, title: 'Tournament list'),
      body: BlocBuilder<TournamentBloc, TournamentCrudState>(
        builder: (BuildContext context, TournamentCrudState state) {
          if (state.currentState == TournamentStates.initial) {
            tourneyBloc.add(TournamentGetListEvent());
          }
          if (state is TournamentsState) {
            if (state.currentState == TournamentStates.completed) {
              tournaments = (state as TournamentsState).tournaments!;
            } else if (state.currentState == TournamentStates.error) {
              return Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const Text("An Error Occured"),
                    ElevatedButton(
                        onPressed: () =>
                            {tourneyBloc.add(TournamentGetListEvent())},
                        child: const Text("Try again."))
                  ],
                ),
              );
            }
            else if (state.currentState == TournamentStates.loading) {
              return const Center(
                child: CircularProgressIndicator(),
              );
            }
          }
          return ListView.builder(
                // Providing a restorationId allows the ListView to restore the
                // scroll position when a user leaves and returns to the app after it
                // has been killed while running in the background.
                restorationId: 'tournamentListView',
                itemCount: tournaments.length,
                itemBuilder: (BuildContext context, int index) {
                  final item = tournaments[index];

                  return ListTile(
                      title: Text('${item.name} (ID: ${item.id})'),
                      leading: const CircleAvatar(
                        // Display the Flutter Logo image asset.
                        foregroundImage:
                            AssetImage('assets/images/flutter_logo.png'),
                      ),
                      onTap: () {
                        // Navigate to the details page. If the user leaves and returns to
                        // the app after it has been killed while running in the
                        // background, the navigation stack is restored.
                        Navigator.pushNamed(
                            // Navigator.restorablePushNamed(
                            context,
                            TournamentScreen.routeName,
                            arguments: {"tournamentId": item.id});
                      });
                },
              );
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => {
          tourneyBloc.add(TournamentGetListEvent())
        },
        child: const Icon(
          Icons.refresh
        ),
      ),
    );
  }
}
