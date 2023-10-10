import 'package:flutter/material.dart';
import 'package:tourney_planner/src/models/team.dart';
import 'package:tourney_planner/src/screens/utility/custom_app_bar.dart';

class TeamScreen extends StatelessWidget {
  final TeamDto data;
  const TeamScreen({super.key, required this.data});
  static const routeName = '/team';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: customAppBar(context: context, title: 'Team overview'),
      body: ListView.builder(
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
      ),
    );
  }
}
