import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:graphview/GraphView.dart';
import 'package:tourney_planner/src/blocs/tournament_bloc.dart';
import 'package:tourney_planner/src/events/tournament_event.dart';
import 'package:tourney_planner/src/models/matchup.dart';
import 'package:tourney_planner/src/models/team.dart';
import 'package:tourney_planner/src/models/tournament.dart';
import 'package:tourney_planner/src/screens/team/team_screen.dart';
import 'package:tourney_planner/src/screens/utility/custom_app_bar.dart';
import 'package:tourney_planner/src/states/tournament_state.dart';

class TournamentScreen extends StatefulWidget {
  final int tournamentId;
  const TournamentScreen({super.key, required this.tournamentId});

  static const routeName = '/tournament';

  @override
  State<TournamentScreen> createState() => _TournamentState(tournamentId);
}

class _TournamentState extends State<TournamentScreen> {

  final Graph graph = Graph()..isTree = true;
  BuchheimWalkerConfiguration builder = BuchheimWalkerConfiguration();
    
  final int tournamentId;
  _TournamentState(this.tournamentId);

  late TournamentDto tournament;

  @override
  void initState() {
    super.initState();

    // 1 = TOP_BOTTOM
    // 2 = BOTTOM_TOP
    // 3 = LEFT_RIGHT
    // 4 = RIGHT_LEFT
    builder.orientation = BuchheimWalkerConfiguration.ORIENTATION_RIGHT_LEFT;

    // Can be used for styling seperation between edges depending on team amount in tournament
    builder.siblingSeparation = 100;
    builder.levelSeparation = 100;
    builder.subtreeSeparation = 150;
  }

  @override
  Widget build(BuildContext context) {
    TournamentBloc tourneyBloc = context.read<TournamentBloc>();
    tourneyBloc.add(TournamentGetByIdEvent(tournamentId));
    return Scaffold(
      // appBar: customAppBar(context: context, title: tournament.name ),
      appBar: customAppBar(context: context, title: "Dummy"),
      body: BlocBuilder<TournamentBloc, TournamentCrudState>(
        builder: (BuildContext context, TournamentCrudState state) {
          if (state.currentState == TournamentStates.initial) {
            tourneyBloc.add(TournamentGetByIdEvent(tournamentId));
          }
          if (state is TournamentState) {
            if (state.currentState == TournamentStates.completed) {
              tournament = (state as TournamentState).tournament!;
              for (var element in tournament.matchups.reversed) {
                if (element.nextMatchupId != null) {
                  graph.addEdge(
                      Node.Id(element.nextMatchupId), Node.Id(element.id));
                } else {
                  graph.addNode(Node.Id(element.id));
                }
              }
              return InteractiveViewer(
                constrained: false,
                boundaryMargin: EdgeInsets.all(100),
                minScale: 0.01,
                maxScale: 5.6,
                child: GraphView(
                  graph: graph,
                  algorithm: BuchheimWalkerAlgorithm(
                      builder, TreeEdgeRenderer(builder)),
                  paint: Paint()
                    ..color = Colors.green
                    ..strokeWidth = 1
                    ..style = PaintingStyle.stroke,
                  builder: (Node node) {
                    // I can decide what widget should be shown here based on the id
                    var id = node.key!.value as int;
                    MatchupDto matchup = tournament.matchups
                        .firstWhere((element) => element.id == id);
                    List<TeamDto> thisTable = tournament.matchups
                        .firstWhere((element) => element.id == id)
                        .teams;

                    return Container(
                      padding: const EdgeInsets.all(16),
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(4),
                        boxShadow: [
                          BoxShadow(color: Colors.blue[100]!, spreadRadius: 1),
                        ],
                      ),
                      child: SingleChildScrollView(
                        child: Column(
                          children: getTable(matchup),
                        ),
                      ),
                    );
                  },
                ),
              );
            } 
          }

          if (state.currentState == TournamentStates.error) {
            return Center(
                child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const Text("An Error Occured"),
                ElevatedButton(
                    onPressed: () => {
                          tourneyBloc
                              .add(TournamentGetByIdEvent(tournamentId))
                        },
                    child: Text("Try again."))
              ],
            ));
          }

          return const Center(
            child: CircularProgressIndicator(),
          );
        },
      ),
    );
  }

  // List<Widget> getTable(MatchupDto matchup) {
  List<Widget> getTable(MatchupDto matchup) {
    List<Widget> teamWidgets = [Text("Matchup ${matchup.id}")];
    for (TeamDto team in matchup.teams) {
      teamWidgets.add(
        Material(
          child: InkWell(
            onTap: () {
              Navigator.restorablePushNamed(context, TeamScreen.routeName,
                  arguments: {"teamId": team.id});
            },
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                // Image(image: team.Logo),
                Text(team.name),
                Text(team.score.toString()),
              ],
            ),
          ),
        ),
      );

      teamWidgets.add(const Divider(
        height: 20,
        thickness: 5,
        color: Colors.black,
      ));
    }
    // for (var i = 0; i < teams.length; i++) {}
    return teamWidgets;
  }

  // @override
  // Widget build(BuildContext context) {
  //   return Scaffold(
  //     body:
  //         InteractiveViewer(
  //       constrained: false,
  //       boundaryMargin: EdgeInsets.all(100),
  //       minScale: 0.01,
  //       maxScale: 5.6,
  //       child: GraphView(
  //         graph: graph,
  //         algorithm:
  //             BuchheimWalkerAlgorithm(builder, TreeEdgeRenderer(builder)),
  //         paint: Paint()
  //           ..color = Colors.green
  //           ..strokeWidth = 1
  //           ..style = PaintingStyle.stroke,
  //         builder: (Node node) {
  //           // I can decide what widget should be shown here based on the id
  //           // var id = node.key!.value as String?;
  //           var id = node.key!.value as int;

  //           // List<TeamDto> thisTable = [
  //           //   tournament.matchups.firstWhere((element) => element.id == id).teamA,
  //           //   tournament.matchups.firstWhere((element) => element.id == id).teamB
  //           // ];

  //           List<TeamDto> thisTable = tournament.matchups
  //               .firstWhere((element) => element.id == id)
  //               .teams;

  //           return Container(
  //               padding: EdgeInsets.all(16),
  //               decoration: BoxDecoration(
  //                 borderRadius: BorderRadius.circular(4),
  //                 boxShadow: [
  //                   BoxShadow(color: Colors.blue[100]!, spreadRadius: 1),
  //                 ],
  //               ),
  //               child: SingleChildScrollView(
  //                 child: Column(
  //                   children: getTable(thisTable),
  //                 ),
  //               ));
  //         },
  //       ),
  //     ),
  //   );
  // }

  // @override
  // Widget build(BuildContext context) {

  //   return Scaffold(
  //     appBar: customAppBar(context: context, title: tournament.name),
  //     body: GraphView(
  //               graph: graph,
  //               algorithm:
  //                   BuchheimWalkerAlgorithm(builder, TreeEdgeRenderer(builder)),
  //               paint: Paint()
  //                 ..color = Colors.green
  //                 ..strokeWidth = 1
  //                 ..style = PaintingStyle.stroke,
  //               builder: (Node node) {
  //                 var id = (node.key!.value as int);
  //                 // List<TeamsPerMatchDto> teamsPerMatch = tournament.matchups
  //                 //     .firstWhere((element) => element.id == id)
  //                 //     .teamsPerMatch;
  //                 return rectangleWidget(id.toString());
  //               },
  //             ),

  //   );
  // }

  // Widget rectangleWidget(String? a) {
  //   return InkWell(
  //     onTap: () {
  //       print('clicked');
  //     },
  //     child: Container(
  //         decoration: BoxDecoration(
  //           borderRadius: BorderRadius.circular(4),
  //           boxShadow: [
  //             BoxShadow(color: Colors.blue[100]!, spreadRadius: 1),
  //           ],
  //         ),
  //         child: Text('${a}')),
  //   );
  // }

  // Widget createGraphWidget(
  //     List<TeamsPerMatchDto> matchData, List<TeamDto> teams) {
  //   return SingleChildScrollView(
  //     scrollDirection: Axis.vertical,
  //     child: Column(
  //         children: [
  //           InkWell(
  //             child: Row(
  //               mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //               children: [
  //                 Image.asset('assets/logo_placeholder.jpg'),
  //                 Text(matchData[0].teamName),
  //                 Text(matchData[0].score.toString())
  //               ],
  //             ),
  //             onTap: () {},
  //           ),
  //           const Divider(
  //             color: Colors.black,
  //             height: 2.0,
  //           ),
  //           InkWell(
  //             child: Row(
  //               mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //               children: [
  //                 Image.asset('assets/logo_placeholder.jpg'),
  //                 Text(matchData[1].teamName),
  //                 Text(matchData[1].score.toString())
  //               ],
  //             ),
  //             onTap: () {},
  //           ),
  //         ],
  //       ),

  //   );
  // }

  // Widget rectangleWidget(
  //     List<TeamsPerMatchDto> matchData, List<TeamDto> teams) {
  //   return InkWell(
  //     onTap: () {
  //       print('clicked');
  //     },
  //     child: Container(
  //       width: 200,
  //       height: 100,
  //       padding: const EdgeInsets.all(16),
  //       decoration: BoxDecoration(
  //         borderRadius: BorderRadius.circular(4),
  //         boxShadow: [
  //           BoxShadow(color: Colors.black, spreadRadius: 1),
  //         ],
  //       ),
  //       child: SingleChildScrollView(
  //         child: Column(
  //           children: [
  //             Row(
  //               mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //               children: [
  //                 Image.asset('assets/logo_placeholder.jpg'),
  //                 Text(matchData[0].teamName),
  //                 Text(matchData[0].score.toString())
  //               ],
  //             ),
  //             const Divider(
  //               color: Colors.black,
  //               height: 2.0,
  //             ),
  //             Row(
  //               mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //               children: [
  //                 Image.asset('assets/logo_placeholder.jpg'),
  //                 Text(matchData[1].teamName),
  //                 Text(matchData[1].score.toString())
  //               ],
  //             ),
  //           ],
  //         ),
  //       ),
  //     ),
  //   );
  // return SizedBox(
  //   width: 100,
  //   height: 100,
  //   child: Column(
  //     children: [
  //       Row(
  //         mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //         children: [
  //           Text(matchData[0].teamName),
  //           Text(matchData[0].score.toString())
  //         ],
  //       ),
  //       const Divider(
  //         color: Colors.black,
  //         height: 2.0,
  //       ),
  //       Row(
  //         mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //         children: [
  //           Text(matchData[1].teamName),
  //           Text(matchData[1].score.toString())
  //         ],
  //       ),
  //     ],
  //   ),
  // );
  // return ListView.builder(
  //       scrollDirection: Axis.horizontal,
  //       shrinkWrap: true,
  //       itemCount: matchData.length,
  //       itemBuilder: (BuildContext ctxt, int index) {
  //         return InkWell(
  //           child: Text(
  //               '${matchData[index].teamName} ${matchData[index].score}'),
  //           onTap: () {
  //             Navigator.restorablePushNamed(context, TeamScreen.routeName,
  //                 arguments: teams[index]);
  //           },
  //         );
  //       },
  //     );
  // return SizedBox(
  //   height: 50,
  //   width: 100,
  //   child: Container(
  //     margin: const EdgeInsets.all(15.0),
  //     padding: const EdgeInsets.all(3.0),
  //     decoration: BoxDecoration(border: Border.all(color: Colors.black)),
  //     child: InkWell(
  //       onTap: () {},
  //       child: Column(
  //         mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //         children: [
  //           Row(
  //             mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //             children: [
  //               Text(matchData[0].teamName),
  //               Text(matchData[0].score.toString())
  //             ],
  //           ),
  //           const Divider(
  //             color: Colors.black,
  //             height: 2.0,
  //           ),
  //           Row(
  //             mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //             children: [
  //               Text(matchData[1].teamName),
  //               Text(matchData[1].score.toString())
  //             ],
  //           ),
  //         ],
  //       ),
  //     ),
  //   ),
  // );
  // return ListView.builder(
  //   // Providing a restorationId allows the ListView to restore the
  //   // scroll position when a user leaves and returns to the app after it
  //   // has been killed while running in the background.
  //   restorationId: 'tournamentListView',
  //   itemCount: matchData.length,
  //   scrollDirection: Axis.vertical,
  //   shrinkWrap: true,
  //   itemBuilder: (BuildContext context, int index) {
  //     final item = matchData[index];
  //     return SizedBox(
  //       height: 100,
  //       width: 200,
  //       child: InkWell(
  //         onTap: () {},
  //         child: Row(
  //           mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //           children: [Text(item.teamName), Text(item.score.toString())],
  //         ),
  //       ),
  //     );
  // return ListTile(
  //     // Title can be used for team name instead and team logo as leading attribute
  //     leading: Text(item.teamName),
  //     trailing: Text(item.score.toString()),
  //     // leading: const CircleAvatar(
  //     //   // Display the team logo
  //     //   foregroundImage: AssetImage('assets/images/flutter_logo.png'),
  //     // ),
  //     onTap: () {
  //       // Navigate to the details page. If the user leaves and returns to
  //       // the app after it has been killed while running in the
  //       // background, the navigation stack is restored.
  //       Navigator.restorablePushNamed(
  //         context,
  //         TeamScreen.routeName,
  //         // Add event to the team bloc
  //         // arguments: item.teamId
  //       );
  //     });
  // }
}
