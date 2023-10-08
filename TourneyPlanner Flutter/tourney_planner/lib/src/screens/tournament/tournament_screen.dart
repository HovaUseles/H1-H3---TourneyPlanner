import 'package:flutter/material.dart';
import 'package:graphview/GraphView.dart';
import 'package:tourney_planner/src/models/matchup.dart';
import 'package:tourney_planner/src/models/teams_per_match.dart';
import 'package:tourney_planner/src/models/tournament.dart';
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
    final node1 = Node.Id(1);
    final node2 = Node.Id(2);
    final node3 = Node.Id(3);
    final node4 = Node.Id(4);
    final node5 = Node.Id(5);
    final node6 = Node.Id(6);
    final node7 = Node.Id(7);
    final node8 = Node.Id(8);
    final node9 = Node.Id(9);
    final node10 = Node.Id(10);
    final node11 = Node.Id(11);
    final node12 = Node.Id(12);
    final node13 = Node.Id(13);
    final node14 = Node.Id(14);

  @override
  Widget build(BuildContext context) {
    List<Node> nodes = [];
    
    graph.addEdge(node12, node14);
    graph.addEdge(node11, node14);
    graph.addEdge(node10, node13);
    graph.addEdge(node9, node13);
    graph.addEdge(node8, node12);
    graph.addEdge(node7, node12);
    graph.addEdge(node6, node11);
    graph.addEdge(node5, node11);
    graph.addEdge(node4, node10);
    graph.addEdge(node3, node10);
    graph.addEdge(node2, node9);
    graph.addEdge(node1, node9);
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
      MatchupDto(id: 2, round: 1, nextMatchupId: 6, teamsPerMatch: matchup2),
      MatchupDto(id: 3, round: 1, nextMatchupId: 7, teamsPerMatch: matchup3),
      MatchupDto(id: 4, round: 1, nextMatchupId: 8, teamsPerMatch: matchup4),
      MatchupDto(id: 5, round: 2, nextMatchupId: 9, teamsPerMatch: matchup5),
      MatchupDto(id: 6, round: 2, nextMatchupId: 10, teamsPerMatch: matchup6),
      MatchupDto(id: 7, round: 3, nextMatchupId: 11, teamsPerMatch: matchup7),
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

    // Can be used for styling seperation between edges depending on team amount in tournament
    if (tournament.matchups.length <= 8) {
      builder.siblingSeparation = 25;
      builder.levelSeparation = 35;
      builder.subtreeSeparation = 70;
    }

    for (var element in tournament.matchups) {
      nodes.add(Node.Id(element));
    }

    // 1 = TOP_BOTTOM
    // 2 = BOTTOM_TOP
    // 3 = LEFT_RIGHT
    // 4 = RIGHT_LEFT
    builder.orientation = 4;

    return Scaffold(
        appBar: customAppBar(context: context, title: tournament.name),
        body: Column(
          mainAxisSize: MainAxisSize.max,
          children: [
            Expanded(
              child: InteractiveViewer(
                  constrained: false,
                  boundaryMargin: const EdgeInsets.all(100),
                  minScale: 0.01,
                  maxScale: 7.5,
                  child: GraphView(
                    graph: graph,
                    algorithm: BuchheimWalkerAlgorithm(
                        builder, TreeEdgeRenderer(builder)),
                    paint: Paint()
                      ..color = Colors.green
                      ..strokeWidth = 1
                      ..style = PaintingStyle.stroke,
                    builder: (Node node) {
                      return rectangleWidget(node.key!.value);
                    },
                  )),
            ),
          ],
        ));
  }

  Widget rectangleWidget(List<TeamsPerMatchDto> matchData) {
    return Container(
      padding: const EdgeInsets.all(8),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(4),
        boxShadow: [
          BoxShadow(color: Colors.blue[100]!, spreadRadius: 1),
        ],
      ),
      // Find a way to do this dynamically if more than 2 teams
      child: Column(
        children: [
          InkWell(
            onTap: () {},
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(matchData[0].teamName),
                Text(matchData[0].score.toString())
              ],
            ),
          ),
          const Divider(
            color: Colors.black,
            height: 2.0,
          ),
          InkWell(
            onTap: () {},
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(matchData[1].teamName),
                Text(matchData[1].score.toString())
              ],
            ),
          ),
        ],
      ),
    );
  }
}
