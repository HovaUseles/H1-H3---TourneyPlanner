import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:tourney_planner/src/blocs/team_bloc.dart';
import 'package:tourney_planner/src/events/team_event.dart';
import 'package:tourney_planner/src/models/team.dart';
import 'package:tourney_planner/src/screens/utility/custom_app_bar.dart';
import 'package:tourney_planner/src/states/team_state.dart';

class TeamScreen extends StatelessWidget {
  // final TeamDto data;
  // const TeamScreen({super.key, required this.data});
  final int teamId;
  const TeamScreen({super.key, required this.teamId});
  static const routeName = '/team';

  @override
  Widget build(BuildContext context) {
    final teamBloc = context.read<TeamBloc>();
    teamBloc.add(TeamGetEvent(teamId));

    return Scaffold(
      appBar: customAppBar(context: context, title: 'Team overview'),
      body: BlocBuilder<TeamBloc, TeamState>(
        builder: (BuildContext context, TeamState state) {
          if (state.currentState == TeamStates.completed) {
            final data = state.team!;
            return ListView.builder(
              // Providing a restorationId allows the ListView to restore the
              // scroll position when a user leaves and returns to the app after it
              // has been killed while running in the background.
              restorationId: 'teamListView',
              itemCount: data.players.length,
              shrinkWrap: true,
              itemBuilder: (BuildContext context, int index) {
                final item = data.players[index];

                return ListTile(
                  title: Text('${item.firstName} ${item.lastName}'),
                  leading: const CircleAvatar(
                    // Display the Flutter Logo image asset.
                    foregroundImage: AssetImage('assets/user_logo.jpg'),
                  ),
                );
              },
            );
          }
          else if (state.currentState == TeamStates.error) {
            return Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text("An Error Occured"),
                  ElevatedButton(
                    onPressed: () => {
                      teamBloc.add(TeamGetEvent(teamId))
                    }, 
                    child: Text("Try again."))
                ],
              ) 
            );
          }

          return const Center(
            child: CircularProgressIndicator(),
          );
          

        },
      ),
    );
  }
}
