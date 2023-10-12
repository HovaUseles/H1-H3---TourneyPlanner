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
  TournamentBloc tourneyBloc = TournamentBloc();

  final int tournamentId;

  _TournamentState(this.tournamentId);

  late TournamentDto tournament;
  @override
  void initState() {
    super.initState();
    
    tourneyBloc.add(TournamentGetByIdEvent(tournamentId));

    // 1 = TOP_BOTTOM
    // 2 = BOTTOM_TOP
    // 3 = LEFT_RIGHT
    // 4 = RIGHT_LEFT
    builder.orientation = BuchheimWalkerConfiguration.ORIENTATION_RIGHT_LEFT;

    // Can be used for styling seperation between edges depending on team amount in tournament
    builder.siblingSeparation = 50;
    builder.levelSeparation = 150;
    builder.subtreeSeparation = 80;
  }

  @override
  void dispose() {
    super.dispose();
    tourneyBloc.close();
  }

  @override
  Widget build(BuildContext context) {
    // TournamentBloc tourneyBloc = context.read<TournamentBloc>();
    return Scaffold(
      // appBar: customAppBar(context: context, title: tournamentName ?? "Dummy" ),
      appBar: customAppBar(context: context, title: "Tournament"),
      body: BlocBuilder<TournamentBloc, TournamentCrudState>(
        bloc: tourneyBloc,
        buildWhen: (previous, current) => current is TournamentState,
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
                boundaryMargin: const EdgeInsets.all(100),
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
              ),
            );
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
    String matchupText = _getMatchupDescriptor(matchup);
    List<Widget> teamWidgets = [
      Padding(
          padding: const EdgeInsets.only(bottom: 15),
          child: Text(matchupText),
          ),
    ];
    for (TeamDto team in matchup.teams) {
      teamWidgets.add(
        Material(
          child: InkWell(
            onTap: () {
              Navigator.restorablePushNamed(context, TeamScreen.routeName,
                  arguments: {"teamId": team.id});
            },
            child: Padding(
              padding: const EdgeInsets.all(15),
              child: SizedBox(
                width: 110,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    // Image(image: team.Logo),
                    Text(team.name),
                    Text(team.score.toString()),
                  ],
                ),
              ),
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
    return teamWidgets;
  }

  String _getMatchupDescriptor(MatchupDto matchup) {
    List<MatchupDto> dummyMatches = tournament.matchups;
    dummyMatches.sort((m1, m2) => m2.round - m1.round);
    int highestRoundValue = dummyMatches.first.round;

    switch(highestRoundValue - matchup.round) {
      case 0:
        return "Final";
      case 1:
        return "Semi-final";
      case 2:
        return "Quarter-final";
      default:
        return "Matchup ${matchup.id}";
    }
  }
}
