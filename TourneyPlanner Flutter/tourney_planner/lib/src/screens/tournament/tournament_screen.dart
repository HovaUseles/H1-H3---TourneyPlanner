import 'package:flutter/material.dart';
import 'package:graphview/GraphView.dart';
import 'package:tourney_planner/src/models/matchup.dart';
import 'package:tourney_planner/src/models/teams_per_match.dart';
import 'package:tourney_planner/src/models/tournament.dart';
import 'package:tourney_planner/src/screens/team/team_screen.dart';
import 'package:tourney_planner/src/screens/utility/custom_app_bar.dart';

class TournamentScreen extends StatefulWidget {
  const TournamentScreen({super.key});

  static const routeName = '/tournament';

  @override
  State<TournamentScreen> createState() => _TournamentState();
}

class _TournamentState extends State<TournamentScreen> {
  final Graph graph = Graph()..isTree = true;
  BuchheimWalkerConfiguration builder = BuchheimWalkerConfiguration();

  @override
  Widget build(BuildContext context) {
    List<TeamsPerMatchDto> matchup1 = [
      TeamsPerMatchDto(
          id: 1, score: 3, matchupId: 1, teamId: 1, teamName: 'Team1'),
      TeamsPerMatchDto(
          id: 2, score: 2, matchupId: 1, teamId: 2, teamName: 'Team2')
    ];
    List<TeamsPerMatchDto> matchup2 = [
      TeamsPerMatchDto(
          id: 3, score: 1, matchupId: 2, teamId: 3, teamName: 'Team3'),
      TeamsPerMatchDto(
          id: 4, score: 2, matchupId: 2, teamId: 4, teamName: 'Team4')
    ];
    List<TeamsPerMatchDto> matchup3 = [
      TeamsPerMatchDto(
          id: 5, score: 1, matchupId: 3, teamId: 5, teamName: 'Team5'),
      TeamsPerMatchDto(
          id: 6, score: 2, matchupId: 3, teamId: 6, teamName: 'Team6')
    ];
    List<TeamsPerMatchDto> matchup4 = [
      TeamsPerMatchDto(
          id: 7, score: 1, matchupId: 4, teamId: 7, teamName: 'Team7'),
      TeamsPerMatchDto(
          id: 8, score: 2, matchupId: 4, teamId: 8, teamName: 'Team8')
    ];
    List<TeamsPerMatchDto> matchup5 = [
      TeamsPerMatchDto(
          id: 9, score: 1, matchupId: 5, teamId: 1, teamName: 'Team1'),
      TeamsPerMatchDto(
          id: 10, score: 2, matchupId: 5, teamId: 4, teamName: 'Team4')
    ];
    List<TeamsPerMatchDto> matchup6 = [
      TeamsPerMatchDto(
          id: 11, score: 1, matchupId: 6, teamId: 6, teamName: 'Team6'),
      TeamsPerMatchDto(
          id: 12, score: 2, matchupId: 6, teamId: 8, teamName: 'Team8')
    ];
    List<TeamsPerMatchDto> matchup7 = [
      TeamsPerMatchDto(
          id: 13, score: 1, matchupId: 7, teamId: 4, teamName: 'Team4'),
      TeamsPerMatchDto(
          id: 14, score: 2, matchupId: 7, teamId: 8, teamName: 'Team8')
    ];

    List<MatchupDto> matches = [
      MatchupDto(id: 1, round: 1, nextMatchupId: 5, teamsPerMatch: matchup1),
      MatchupDto(id: 2, round: 1, nextMatchupId: 5, teamsPerMatch: matchup2),
      MatchupDto(id: 3, round: 1, nextMatchupId: 6, teamsPerMatch: matchup3),
      MatchupDto(id: 4, round: 1, nextMatchupId: 6, teamsPerMatch: matchup4),
      MatchupDto(id: 5, round: 2, nextMatchupId: 7, teamsPerMatch: matchup5),
      MatchupDto(id: 6, round: 2, nextMatchupId: 7, teamsPerMatch: matchup6),
      MatchupDto(id: 7, round: 3, nextMatchupId: 7, teamsPerMatch: matchup7),
    ];

    TournamentDto tournament = TournamentDto(
        id: 1,
        name: 'Test tournament',
        startDate: DateTime.now(),
        createdById: 1,
        createdByName: 'Tester',
        gameTypeId: 1,
        gameTypeName: 'Football',
        tournamentTypeId: 1,
        tournamentTypeName: 'Knockout',
        matchups: matches);

    List<Node> nodes = [];

    // Fails if done in the same loop since it isn't guaranteed a nextMatchupId node has been created yet
    for (var element in tournament.matchups) {
      graph.addNode(Node.Id(element.id));
    }
    for (var element in tournament.matchups) {
      graph.addEdge(Node.Id(element.id), Node.Id(element.nextMatchupId));
      // if (element.nextMatchupId != 0) {}
    }

    // 1 = TOP_BOTTOM
    // 2 = BOTTOM_TOP
    // 3 = LEFT_RIGHT
    // 4 = RIGHT_LEFT
    builder.orientation = 4;

    // Can be used for styling seperation between edges depending on team amount in tournament
    if (tournament.matchups.length <= 8) {
      builder.siblingSeparation = 100;
      builder.levelSeparation = 100;
      builder.subtreeSeparation = 150;
    }

    return Scaffold(
      appBar: customAppBar(context: context, title: tournament.name),
      body: Column(
        mainAxisSize: MainAxisSize.max,
        children: [
          Expanded(
            child: InteractiveViewer(
              constrained: false,
              boundaryMargin: const EdgeInsets.all(50),
              minScale: 0.01,
              maxScale: 7.5,
              child: GraphView(
                graph: graph,
                algorithm:
                    BuchheimWalkerAlgorithm(builder, TreeEdgeRenderer(builder)),
                paint: Paint()
                  ..color = Colors.green
                  ..strokeWidth = 1
                  ..style = PaintingStyle.stroke,
                builder: (Node node) {
                  var id = (node.key!.value as int);
                  List<TeamsPerMatchDto> teams = tournament.matchups
                      .firstWhere((element) => element.id == id)
                      .teamsPerMatch;
                  return rectangleWidget(teams);
                },
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget rectangleWidget(List<TeamsPerMatchDto> matchData) {
    return SizedBox(
      height: 100.0,
      width: 100.0,
      child: ListView.builder(
        scrollDirection: Axis.horizontal,
        itemCount: matchData.length,
        itemBuilder: (BuildContext ctxt, int index) {
          return Text('${matchData[index].teamName} ${matchData[index].score}');
        },
      ),
    );
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
  }
}
