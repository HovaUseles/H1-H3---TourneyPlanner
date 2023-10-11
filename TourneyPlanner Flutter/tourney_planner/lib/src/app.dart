import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:tourney_planner/src/models/team.dart';
import 'package:tourney_planner/src/models/tournament.dart';
import 'package:tourney_planner/src/screens/login/login_screen.dart';
import 'package:tourney_planner/src/screens/team/team_screen.dart';
import 'package:tourney_planner/src/screens/tournament/tournament_list_screen.dart';
import 'package:tourney_planner/src/screens/utility/slide_animation_route.dart';
import 'package:tourney_planner/src/screens/tournament/tournament_screen.dart';
import 'controllers/settings_controller.dart';
import 'screens/settings/settings_view.dart';

/// The Widget that configures your application.
class MyApp extends StatelessWidget {
  const MyApp({
    super.key,
    required this.settingsController,
  });

  final SettingsController settingsController;

  @override
  Widget build(BuildContext context) {
    // Glue the SettingsController to the MaterialApp.
    //
    // The ListenableBuilder Widget listens to the SettingsController for changes.
    // Whenever the user updates their settings, the MaterialApp is rebuilt.
    // return ListenableBuilder(
    //   listenable: settingsController,
    //   builder: (BuildContext listenContext, Widget? child) {
    //     return MaterialApp(
          
    //       // Providing a restorationScopeId allows the Navigator built by the
    //       // MaterialApp to restore the navigation stack when a user leaves and
    //       // returns to the app after it has been killed while running in the
    //       // background.
    //       restorationScopeId: 'app',

    //       // Provide the generated AppLocalizations to the MaterialApp. This
    //       // allows descendant Widgets to display the correct translations
    //       // depending on the user's locale.
    //       localizationsDelegates: const [
    //         AppLocalizations.delegate,
    //         GlobalMaterialLocalizations.delegate,
    //         GlobalWidgetsLocalizations.delegate,
    //         GlobalCupertinoLocalizations.delegate,
    //       ],
    //       supportedLocales: const [
    //         Locale('en', ''), // English, no country code
    //       ],

    //       // Use AppLocalizations to configure the correct application title
    //       // depending on the user's locale.
    //       //
    //       // The appTitle is defined in .arb files found in the localization
    //       // directory.
    //       onGenerateTitle: (BuildContext context) =>
    //           AppLocalizations.of(context)!.appTitle,

    //       // Define a light and dark color theme. Then, read the user's
    //       // preferred ThemeMode (light, dark, or system default) from the
    //       // SettingsController to display the correct theme.
    //       theme: ThemeData(),
    //       darkTheme: ThemeData.dark(),
    //       themeMode: settingsController.themeMode,

    //       initialRoute: '/tournament',

    //       // Define a function to handle named routes in order to support
    //       // Flutter web url navigation and deep linking.
    //       onGenerateRoute: (RouteSettings routeSettings) {
    //         switch (routeSettings.name) {
    //           case LoginScreen.routeName:
    //             return SlideLeftRoute(
    //               widget: const LoginScreen(),
    //             );
    //           case SettingsView.routeName:
    //             return SlideLeftRoute(
    //               widget: SettingsView(controller: settingsController),
    //             );
    //           case TournamentScreen.routeName:
    //             // return SlideLeftRoute(widget: const TournamentScreen());
    //             return SlideLeftRoute(widget: const InkWell(child: Text("Works"),));
    //         }
    //         return SlideLeftRoute(widget: const TournamentListScreen());
    //       },
    //     );
    //   },
    // );


    return MaterialApp(
          
          // Providing a restorationScopeId allows the Navigator built by the
          // MaterialApp to restore the navigation stack when a user leaves and
          // returns to the app after it has been killed while running in the
          // background.
          restorationScopeId: 'app',

          // Provide the generated AppLocalizations to the MaterialApp. This
          // allows descendant Widgets to display the correct translations
          // depending on the user's locale.
          localizationsDelegates: const [
            AppLocalizations.delegate,
            GlobalMaterialLocalizations.delegate,
            GlobalWidgetsLocalizations.delegate,
            GlobalCupertinoLocalizations.delegate,
          ],
          supportedLocales: const [
            Locale('en', ''), // English, no country code
          ],

          // Use AppLocalizations to configure the correct application title
          // depending on the user's locale.
          //
          // The appTitle is defined in .arb files found in the localization
          // directory.
          onGenerateTitle: (BuildContext context) =>
              AppLocalizations.of(context)!.appTitle,

          // Define a light and dark color theme. Then, read the user's
          // preferred ThemeMode (light, dark, or system default) from the
          // SettingsController to display the correct theme.
          theme: ThemeData(),
          darkTheme: ThemeData.dark(),
          themeMode: settingsController.themeMode,

          // initialRoute: '/tournament/',
          initialRoute: TournamentListScreen.routeName,
          // Define a function to handle named routes in order to support
          // Flutter web url navigation and deep linking.
          onGenerateRoute: (RouteSettings routeSettings) {
            switch (routeSettings.name) {
              case LoginScreen.routeName:
                return SlideLeftRoute(
                  widget: const LoginScreen(),
                );
              case SettingsView.routeName:
                return SlideLeftRoute(
                  widget: SettingsView(controller: settingsController),
                );
              case TournamentScreen.routeName:
                final args = routeSettings.arguments as Map<String, dynamic>;
                int? tournamentId = args["tournamentId"];
                if(tournamentId == null) throw Exception("Could not route, Tournament id was null");
                return SlideLeftRoute(widget: TournamentScreen(tournamentId: tournamentId));
              case TeamScreen.routeName:
                // final args = ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
                // TeamDto team = TeamDto.fromMap(args);
                // final args = ModalRoute.of(context)!.settings.arguments as Map<String, int>;
                final args = routeSettings.arguments as Map<String, int>;
                int? teamId = args["teamId"];
                if(teamId == null) throw Exception("Could not route, Team id was null");
                return SlideLeftRoute(widget: TeamScreen(teamId: teamId));
            }
            return SlideLeftRoute(widget: const TournamentListScreen());
          },
          // home: InkWell(child: Text("Test"),)
        );
  }
}
